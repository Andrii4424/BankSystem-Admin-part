using Application.DTO;
using Application.DTO.BankProductDto;
using Application.ServiceContracts.BankServiceContracts;
using AutoMapper;
using Domain.Entities.Banks;
using Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankService
{
    public class BankUpdateService : IBankUpdateService
    {
        private readonly IBankRepository _bankRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<BankUpdateService> _logger;

        public BankUpdateService(IBankRepository bankRepository, IMapper mapper, IWebHostEnvironment env, ILogger<BankUpdateService> logger)
        {
            _bankRepository = bankRepository;
            _mapper = mapper;
            _env = env;
            _logger = logger;
        }

        public async Task<OperationResult> UpdateBankAsync(Guid bankId, BankDto bankDto, IFormFile? bankLogo)
        {
            _logger.LogInformation("Attempting to update bank with ID {BankId}", bankId);
            BankEntity? bank = await _bankRepository.GetValueByIdAsync(bankId);
            if (bank == null)
            {
                _logger.LogError("Bank with ID {BankId} not found for update", bankId);
                throw new NullReferenceException("This bank doesnt exist");
            }
            bankDto.EstablishedDate = bank.EstablishedDate;
            if (bank.BankName != bankDto.BankName && await _bankRepository.IsUnique(b => b.BankName == bankDto.BankName))
            {
                _logger.LogWarning("Bank with name {BankName} is already exist!", bankDto.BankName);
                return OperationResult.Error("Bank with this name is already exist!");
            }

            if (bankLogo!=null ) //Replacing image name if bank name changes
            {
                if (bank.BankLogoPath != "uploads/no-image-icon.svg") //deleting previous logo if its not default
                {
                    string deletePath = Path.Combine(_env.WebRootPath, bank.BankLogoPath);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }
                //Adding new file
                string fileName = $"{bankDto.BankName}{bankLogo.FileName.ToLower()}";
                string createPath = Path.Combine(_env.WebRootPath, "uploads", "bank-logo", fileName);
                using (var stream = new FileStream(createPath, FileMode.Create))
                {
                    await bankLogo.CopyToAsync(stream);
                }
                bankDto.BankLogoPath = $"uploads/bank-logo/{fileName}";
                _logger.LogInformation("Banks {BankId} logo has been successfully updated", bankId);
            }
            else if(bankDto.BankName != bank.BankName)
            {
                string newPath = Path.Combine(_env.WebRootPath, bank.BankLogoPath.Replace(bank.BankName, bankDto.BankName));
                string oldPath = Path.Combine(_env.WebRootPath, bank.BankLogoPath);
                if (File.Exists(oldPath))
                {
                    File.Move(oldPath, newPath);
                    bankDto.BankLogoPath = bank.BankLogoPath.Replace(bank.BankName, bankDto.BankName); 
                    _logger.LogInformation("Banks {BankId} logo path has been successfully renamed", bankId);
                }
            }
            if(bankDto.BankLogoPath == null) bankDto.BankLogoPath = bank.BankLogoPath;

            bank = _mapper.Map(bankDto, bank);
            _bankRepository.UpdateObject(bank);
            await _bankRepository.SaveAsync();

            _logger.LogInformation("Bank with id {BankId} has been successfully updated", bankId);
            return OperationResult.Ok();
        }
    }
}
