using Domain.Abstractions;
using Domain.Entities.Banks;

namespace Application.Services
{
    public static class GeneralServiceMethods<T> where T : class, IHasBankName
    {
        public static List<T>? ToListWithBankName(List<T>? childList, List<BankEntity>? banks)
        {
            if (childList!=null && banks!=null)
            {
                foreach (T child in childList)
                {
                    child.BankName = banks.FirstOrDefault(b => b.Id == child.BankId)?.BankName;
                }
            }
            return childList;
        }
    }
}
