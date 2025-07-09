using Application.DTO.BankProductDto;
using Application.ServiceContracts.BankServiceContracts;
using AutoMapper;
using Domain.Entities.Banks;
using Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankServices
{
    public class BankReadService: IBankReadService
    {
        private readonly IBankRepository _bankRepository;
        private readonly IMapper _mapper;
        
        public BankReadService(IBankRepository bankRepository, IMapper mapper)
        {
            _bankRepository = bankRepository;
            _mapper = mapper;
        }

        public async Task<List<BankDto>?> GetBanksListAsync()
        {
            List<BankEntity>? bankEntities = await _bankRepository.GetAllValuesAsync() as List<BankEntity>;
            return _mapper.Map<List<BankDto>>(bankEntities);
        }

        public async Task<BankDto> GetBankByIdAsync(Guid bankdId)
        {
            BankEntity? bankEntity = await _bankRepository.GetValueByIdAsync(bankdId);
            if (bankEntity == null) { throw new NullReferenceException("Bank with this id doesnt exist"); }
            return _mapper.Map<BankDto>(bankEntity);
        }

        public async Task<List<BankDto>> GetLimitedBanksListAsync(int firstElement, int itemsToLoad, string? orderMethod)
        {
            Expression<Func<BankEntity, object>> selector;
            bool asceding = true;
            switch (orderMethod)
            {
                case "oldest":
                    selector = b => b.EstablishedDate;
                    asceding = false;
                    break;
                case "newest":
                    selector = b => b.EstablishedDate;
                    break;
                case "rating-descending":
                    selector = b =>b.Rating;
                    asceding = false;
                    break;
                case "popularity-descending":
                    selector = b => b.ActiveClientsCount;
                    asceding = false; 
                    break;
                default:
                    selector = b => b.EstablishedDate;
                    asceding = false;
                    break;
            }
            return _mapper.Map<List<BankDto>>(await _bankRepository.GetLimitedBankList(firstElement, itemsToLoad, selector, asceding));
        }

        public async Task<int> GetBanksCountAsync(){
            return await _bankRepository.GetElementsCountAsync();
        }

    }
}
