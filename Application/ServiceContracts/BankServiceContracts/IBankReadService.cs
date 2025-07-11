using Application.DTO.BankProductDto;
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
        public Task<List<BankDto>?> GetBanksListAsync();
        public Task<BankDto> GetBankByIdAsync(Guid bankdId);
        public Task<List<BankDto>> GetLimitedBanksListAsync(int firstElement, int itemsToLoad, string? orderMethod,
            bool? licenseFilter, bool? siteFilter, double? ratingFilter, int? clientsCountFilter, int? capitalizationFilter);
        public Task<int> GetBanksCountAsync();
    }
}
