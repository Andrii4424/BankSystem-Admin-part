using Application.DTO.BankProductDto;
using Application.DTO.FiltersDto;
using Domain.Entities.Banks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceContracts.BankServiceContracts
{
    public interface IBankReadService
    {
        public Task<List<BankDto>?> GetLimitedByDtoFilterAsync(BankFilters filters);
        public Task<int> GetCountByDtoFilter(BankFilters filters);
        public Task<string> GetBankNameById(Guid id);
        public Task<List<BankDto>?> GetBanksListAsync();
        public Task<BankDto> GetBankByIdAsync(Guid bankId);
        public Task<List<BankDto>> GetLimitedBanksListAsync(int firstElement, int itemsToLoad, string? searchValue, string? orderMethod,
            bool? licenseFilter, bool? siteFilter, double? ratingFilter, int? clientsCountFilter, int? capitalizationFilter);
        public Task<int> GetBanksCountAsync(string? searchValue, bool? licenseFilter, bool? siteFilter, double? ratingFilter,
            int? clientsCountFilter, int? capitalizationFilter);
        public bool IsObjectMatchesFilters(BankDto bank, string? searchValue, bool? licenseFilter, bool? siteFilter, double? ratingFilter,
            int? clientsCountFilter, int? capitalizationFilter);

    }
}
