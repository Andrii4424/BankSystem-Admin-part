using Application.DTO.BankProductDto;
using Application.DTO.FiltersDto;
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
        private readonly ILogger<BankReadService> _logger;
        
        public BankReadService(IBankRepository bankRepository, IMapper mapper, ILogger<BankReadService> logger)
        {
            _bankRepository = bankRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<BankDto>?> GetLimitedByDtoFilterAsync(BankFilters filters)
        {
            Filters<BankEntity> GeneralFilters = filters.ToGeneralFilters();
            List<BankEntity>? banks = await _bankRepository.GetLimitedAsync(GeneralFilters.FirstItem, GeneralFilters.ElementsToLoad, GeneralFilters.SearchFilter,
                GeneralFilters.Ascending, GeneralFilters.SortValue, GeneralFilters.EntityFilters);
            _logger.LogInformation("User get banks to add another entity");
            return _mapper.Map<List<BankDto>>(banks);
        }


        public async Task<int> GetCountByDtoFilter(BankFilters filters)
        {
            Filters<BankEntity> generalFilters= filters.ToGeneralFilters();
            return await _bankRepository.CountAsync(generalFilters.SearchFilter, generalFilters.EntityFilters);
        }

        public async Task<List<BankDto>?> GetBanksListAsync()
        {
            List<BankEntity>? bankEntities = await _bankRepository.GetAllValuesAsync() as List<BankEntity>;
            _logger.LogInformation("Getting full bank list");
            return _mapper.Map<List<BankDto>>(bankEntities);
        }

        public async Task<BankDto> GetBankByIdAsync(Guid bankId)
        {
            BankEntity? bankEntity = await _bankRepository.GetValueByIdAsync(bankId);
            if (bankEntity == null) {
                _logger.LogError("Bank with ID {BankId} was not found", bankId);
                throw new ArgumentException($"Bank with id {bankId} doesnt exist"); 
            }
            _logger.LogInformation("Bank with ID {BankId} successfully received", bankId);
            return _mapper.Map<BankDto>(bankEntity);
        }

        public async Task<List<BankDto>> GetLimitedBanksListAsync(int firstElement, int itemsToLoad, string? searchValue, string? orderMethod, 
            bool? licenseFilter, bool? siteFilter, double? ratingFilter, int? clientsCountFilter, int? capitalizationFilter)
        {
            Expression<Func<BankEntity, bool>>? searchFilter=GetSearchFilter(searchValue);
            Expression<Func<BankEntity, object>> selector;
            bool asceding;
            GetSelector(out selector, out asceding, orderMethod);
            if (ratingFilter > 5) ratingFilter = ratingFilter / 10;
            List<Expression<Func<BankEntity, bool>>?> filters= GetFilters(licenseFilter, siteFilter, ratingFilter, 
                clientsCountFilter, capitalizationFilter);

            _logger.LogInformation("Limited bank list successfully received");
            return _mapper.Map<List<BankDto>>(await _bankRepository.GetLimitedBankList(firstElement, itemsToLoad, searchFilter, selector, 
                asceding,  filters));
        }

        public async Task<int> GetBanksCountAsync(string? searchValue, bool? licenseFilter, bool? siteFilter, double? ratingFilter, 
            int? clientsCountFilter, int? capitalizationFilter)
        {
            Expression<Func<BankEntity, bool>>? searchFilter = GetSearchFilter(searchValue);
            List<Expression<Func<BankEntity, bool>>?> filters = GetFilters(licenseFilter, siteFilter, ratingFilter,
                clientsCountFilter, capitalizationFilter);

            _logger.LogInformation("Banks list count with filters successfully received");
            return await _bankRepository.CountAsync(searchFilter, filters);
        }

        private Expression<Func<BankEntity, bool>>? GetSearchFilter(string? searchValue)
        {
            //Trim() deleting all the spaces if value contains only one word
            return (searchValue != null && searchValue.Trim() != "0") ? b => b.BankName.Contains(searchValue.Trim()) : null;
        }

        private void GetSelector(out Expression<Func<BankEntity, object>> selector, out bool asceding, string? orderMethod)
        {
            asceding = true;
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
                    selector = b => b.Rating;
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
        }

        public bool IsObjectMatchesFilters(BankDto bank, string? searchValue, bool? licenseFilter, bool? siteFilter, double? ratingFilter,
            int? clientsCountFilter, int? capitalizationFilter) {
            if((licenseFilter ==true && bank.HasLicense==false) || (siteFilter==true && bank.WebsiteUrl==null)) return false;
            else if((ratingFilter.HasValue && bank.Rating<ratingFilter) || (clientsCountFilter.HasValue && bank.ClientsCount < clientsCountFilter) 
                || (capitalizationFilter.HasValue && bank.Capitalization < capitalizationFilter))
            {
                _logger.LogDebug("Bank {BankName} excluded due to missing filters", bank.BankName);
                return false;
            }

            if(searchValue!=null){
                if (bank.BankName.Contains(searchValue))
                {
                    _logger.LogDebug("Bank {BankName} excluded due to missing search filter", bank.BankName);
                    return false;
                }
            }
            _logger.LogDebug("Bank {BankName} successfully checked for filters", bank.BankName);
            return true;
        } 
        public async Task<string> GetBankNameById(Guid id)
        {
            return (await GetBankByIdAsync(id)).BankName;
        }

        private List<Expression<Func<BankEntity, bool>>?> GetFilters(bool? licenseFilter, bool? siteFilter, double? ratingFilter, 
            int? clientsCountFilter, int? capitalizationFilter) {
            List<Expression<Func<BankEntity, bool>>?> filters = new List<Expression<Func<BankEntity, bool>>?>();

            filters.Add((licenseFilter.HasValue && licenseFilter == true) ? b => b.HasLicense : null);
            filters.Add((siteFilter.HasValue && siteFilter == true) ? b => b.WebsiteUrl != null : null);
            filters.Add((ratingFilter.HasValue && ratingFilter != 0) ? b => b.Rating >= ratingFilter : null);
            filters.Add((clientsCountFilter.HasValue && clientsCountFilter != 0) ? b => b.ActiveClientsCount >= clientsCountFilter : null);
            filters.Add((capitalizationFilter.HasValue && capitalizationFilter != 0) ? b => b.Capitalization >= capitalizationFilter : null);

            return filters;
        }
    }
}
