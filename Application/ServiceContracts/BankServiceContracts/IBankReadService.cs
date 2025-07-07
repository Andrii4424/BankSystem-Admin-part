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
        public Task<List<BankDto>?> GetBanksList();
        public Task<BankDto> GetBankById(Guid bankdId);
        public Task<List<BankDto>> GetLimitedBanksList(int firstElement);
    }
}
