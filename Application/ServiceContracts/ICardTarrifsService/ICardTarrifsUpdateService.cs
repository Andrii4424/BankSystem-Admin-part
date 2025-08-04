using Application.DTO;
using Application.DTO.BankProductDto;

namespace Application.ServiceContracts.ICardTarrifsService
{
    public interface ICardTarrifsUpdateService
    {
        public Task<OperationResult> UpdateCardAsync(Guid cardId, CardTariffsDto cardDto);
    }
}
