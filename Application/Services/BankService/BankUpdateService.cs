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

        public BankUpdateService(IBankRepository bankRepository, IMapper mapper)
        {
            _bankRepository = bankRepository;
            _mapper = mapper;
        }

        public async Task UpdateBank(Guid bankId, BankDto bankDto)
        {
            BankEntity? bank = await _bankRepository.GetValueByIdAsync(bankId);
            if(bank == null) throw new NullReferenceException("This bank doesnt exist");
            bank = _mapper.Map(bankDto, bank);
             _bankRepository.UpdateObject(bank);
            await _bankRepository.SaveAsync();
        }
    }
}
