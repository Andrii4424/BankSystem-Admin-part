using Application.DTO;
using Application.DTO.BankProductDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceContracts.BankServiceContracts
{
    public interface IBankAddService
    {
        public Task<OperationResult> AddBankAsync(BankDto bankDto, IFormFile? bankLogo);
    }
}
