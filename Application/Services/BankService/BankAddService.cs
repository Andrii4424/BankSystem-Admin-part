using Application.DTO;
using Application.DTO.BankProductDto;
using Application.ServiceContracts.BankServiceContracts;
using AutoMapper;
using Domain.Entities.Banks;
using Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace Application.Services.BankService
{
    public class BankAddService : IBankAddService
    {
        private readonly IBankRepository _bankRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<BankAddService> _logger;

        public BankAddService (IBankRepository bankRepository, IMapper mapper, IWebHostEnvironment env, ILogger<BankAddService> logger)
        {
            _bankRepository = bankRepository;
            _mapper = mapper;
            _env = env;
            _logger = logger;
        }

        public async Task<OperationResult> AddBankAsync(BankDto bankDto, IFormFile? bankLogo)
        {
            _logger.LogInformation("Attempting to update bank with name {BankName}", bankDto.BankName);
            bankDto.BankName =bankDto.BankName.Trim();
            string[] allowedExtensions = [".jpg", ".jpeg", ".png", ".svg"];
            if (await _bankRepository.IsExists(b => b.BankName == bankDto.BankName))
            {
                _logger.LogWarning("Bank with name {BankName} is already exist!", bankDto.BankName);
                return OperationResult.Error("Bank with this name is already exist!");
            }
            if (bankLogo == null) bankDto.BankLogoPath = "uploads/no-image-icon.svg";
            else if (!allowedExtensions.Contains(Path.GetExtension(bankLogo.FileName.ToLower())))
            {
                _logger.LogWarning("Banks {BankName} logo format incorrect", bankDto.BankName);
                return OperationResult.Error("Invalid image format. Please add an image in .jpg, .jpeg, .png or .svg format.");
            }
            else
            {
                string fileName = $"{bankDto.BankName}{bankLogo.FileName.ToLower()}";
                string absolutePath = Path.Combine(_env.WebRootPath, "uploads", "bank-logo", fileName);
                using(var stream = new FileStream(absolutePath, FileMode.Create))
                {
                    await bankLogo.CopyToAsync(stream);
                }
                bankDto.BankLogoPath = $"uploads/bank-logo/{fileName}";
            }

            await _bankRepository.AddAsync(_mapper.Map<BankEntity>(bankDto));
            await _bankRepository.SaveAsync();

            _logger.LogInformation("Bank with name {BankName} has been successfully added", bankDto.BankName);
            return OperationResult.Ok();
        }
    }
}
