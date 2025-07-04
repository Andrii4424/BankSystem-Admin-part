﻿using Application.DTO.BankProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceContracts.BankServiceContracts
{
    public interface IBankUpdateService
    {
        public Task UpdateBank(Guid bankId, BankDto bankDto);
    }
}
