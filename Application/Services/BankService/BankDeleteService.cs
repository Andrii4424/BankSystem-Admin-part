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

        public BankDeleteService(IBankRepository bankRepository, IMapper mapper)
        {
            _bankRepository = bankRepository;
            _mapper = mapper;
        }

        public async Task DeleteBank(Guid bankId)
        {
            BankEntity? bank = await _bankRepository.GetValueByIdAsync(bankId);
            if (bank == null) _bankRepository.DeleteElement(bank);
            await _bankRepository.SaveAsync();
        }
    }
}
