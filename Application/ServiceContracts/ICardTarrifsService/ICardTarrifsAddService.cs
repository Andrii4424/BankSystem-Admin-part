using Application.DTO;
using Application.DTO.BankProductDto;

namespace Application.ServiceContracts.ICardTarrifsService
{
    public interface ICardTarrifsAddService
    {
        public Task<OperationResult> AddCardAsync(CardTariffsDto cardDto);
    }
}
