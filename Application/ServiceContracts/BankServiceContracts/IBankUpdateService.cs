using Application.DTO;
using Application.DTO.BankProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceContracts.BankServiceContracts
{
    public interface IBankUpdateService
    {
        public Task<OperationResult> UpdateBankAsync(Guid bankId, BankDto bankDto, IFormFile? bankLogo);
    }
}
