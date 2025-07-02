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
            BankEntity bankEntity = _mapper.Map<BankEntity>(bankDto);
             _bankRepository.UpdateObject(bankEntity);
            await _bankRepository.SaveAsync();
        }
    }
}
