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

        public BankUpdateService(IBankRepository bankRepository, IMapper mapper, IWebHostEnvironment env)
        {
            _bankRepository = bankRepository;
            _mapper = mapper;
            _env = env;
        }

        public async Task<OperationResult> UpdateBankAsync(Guid bankId, BankDto bankDto, IFormFile? bankLogo)
        {
            BankEntity? bank = await _bankRepository.GetValueByIdAsync(bankId);
            if(bank == null) throw new NullReferenceException("This bank doesnt exist");
            bankDto.EstablishedDate = bank.EstablishedDate;
            if (bank.BankName != bankDto.BankName && await _bankRepository.IsUnique(b => b.BankName == bankDto.BankName))
            {
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
            }
            else if(bankDto.BankName != bank.BankName)
            {
                string newPath = Path.Combine(_env.WebRootPath, bank.BankLogoPath.Replace(bank.BankName, bankDto.BankName));
                string oldPath = Path.Combine(_env.WebRootPath, bank.BankLogoPath);
                if (File.Exists(oldPath))
                {
                    File.Move(oldPath, newPath);
                    bankDto.BankLogoPath = bank.BankLogoPath.Replace(bank.BankName, bankDto.BankName);
                }
            }
            if(bankDto.BankLogoPath == null) bankDto.BankLogoPath = bank.BankLogoPath;
            bank = _mapper.Map(bankDto, bank);
            _bankRepository.UpdateObject(bank);
            await _bankRepository.SaveAsync();
            return OperationResult.Ok();
        }
    }
}
