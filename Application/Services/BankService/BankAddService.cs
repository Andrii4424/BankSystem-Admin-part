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
    public class BankAddService : IBankAddService
    {
        private readonly IBankRepository _bankRepository;
        private readonly IMapper _mapper;

        public BankAddService (IBankRepository bankRepository, IMapper mapper)
        {
            _bankRepository = bankRepository;
            _mapper = mapper;
        }

        public async Task AddBank(BankDto bankDto)
        {
            await _bankRepository.AddAsync(_mapper.Map<BankEntity>(bankDto));
            await _bankRepository.SaveAsync();
        }
    }
}
