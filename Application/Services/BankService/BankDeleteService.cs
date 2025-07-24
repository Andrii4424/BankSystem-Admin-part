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
    public class BankDeleteService: IBankDeleteService
    {
        private readonly IBankRepository _bankRepository;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<BankDeleteService> _logger;

        public BankDeleteService(IBankRepository bankRepository, IWebHostEnvironment env, ILogger<BankDeleteService> logger)
        {
            _bankRepository = bankRepository;
            _logger = logger;
            _env = env;
        }

        public async Task DeleteBankAsync(Guid bankId)
        {
            BankEntity? bank = await _bankRepository.GetValueByIdAsync(bankId);
            if (bank != null) { 
                if(bank.BankLogoPath!= "uploads/no-image-icon.svg")
                {
                    string path = Path.Combine(_env.WebRootPath, bank.BankLogoPath);
                    if (File.Exists(path)) { 
                        File.Delete(path);
                    }
                }
                _bankRepository.DeleteElement(bank);
                _logger.LogInformation("Bank with id {BankId} has been successfully deleted", bankId);
            }
            await _bankRepository.SaveAsync();
        }
    }
}
