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
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BankDeleteService(IBankRepository bankRepository, IMapper mapper, IWebHostEnvironment env)
        {
            _bankRepository = bankRepository;
            _mapper = mapper;
            _env = env;
        }

        public async Task DeleteBankAsync(Guid bankId)
        {
            BankEntity? bank = await _bankRepository.GetValueByIdAsync(bankId);
            if (bank != null) { 
                if(bank.BankLogoPath!= "/uploads/no-image-icon.svg")
                {
                    string hui = _env.WebRootPath;
                    string path = Path.Combine(_env.WebRootPath, bank.BankLogoPath);
                    if (File.Exists(path)) { 
                        File.Delete(path);
                    }
                }
                _bankRepository.DeleteElement(bank);
            } 
            await _bankRepository.SaveAsync();
        }
    }
}
