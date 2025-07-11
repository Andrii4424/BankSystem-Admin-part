using Application.DTO.BankProductDto;
using Application.ServiceContracts.BankServiceContracts;
using AutoMapper;
using Domain.Entities.Banks;
using Domain.RepositoryContracts;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<List<BankDto>> GetLimitedBanksListAsync(int firstElement, int itemsToLoad, string? orderMethod, 
            bool? licenseFilter, bool? siteFilter, double? ratingFilter, int? clientsCountFilter, int? capitalizationFilter)
        {
            Expression<Func<BankEntity, object>> selector;
            List<Expression<Func<BankEntity, bool>>?> filters= new List<Expression<Func<BankEntity, bool>>?>();

            filters.Add((licenseFilter.HasValue && licenseFilter == true) ? b => b.HasLicense : null);
            filters.Add((siteFilter.HasValue && siteFilter == true) ? b => b.WebsiteUrl!=null: null);
            filters.Add((ratingFilter.HasValue && ratingFilter!=0) ? b => b.Rating>ratingFilter: null);
            filters.Add((clientsCountFilter.HasValue && clientsCountFilter!=0) ? b => b.ActiveClientsCount > clientsCountFilter : null);
            filters.Add((capitalizationFilter.HasValue && capitalizationFilter != 0) ? b => b.Capitalization > capitalizationFilter : null);

            bool asceding = true;
            switch (orderMethod)
            {
                case "oldest":
                    selector = b => b.EstablishedDate;
                    break;
                case "newest":
                    selector = b => b.EstablishedDate;
                    asceding = false;
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
                    break;
            }
            return _mapper.Map<List<BankDto>>(await _bankRepository.GetLimitedBankList(firstElement, itemsToLoad, selector, asceding, filters));
        }

        public async Task<int> GetBanksCountAsync(){
            return await _bankRepository.GetElementsCountAsync();
        }


    }
}
