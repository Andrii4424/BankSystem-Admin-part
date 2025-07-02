using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceContracts.BankServiceContracts
{
    public interface IBankDeleteService
    {
        public Task DeleteBank(Guid bankId);
    }
}
