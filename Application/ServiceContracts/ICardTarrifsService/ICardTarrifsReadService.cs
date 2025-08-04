using Application.DTO.BankProductDto;
using Application.DTO.FiltersDto;

namespace Application.ServiceContracts.ICardTarrifsService
{
    public interface ICardTarrifsReadService
    {
        public Task<List<CardTariffsDto>?> GetCardsAsync(CardTariffsFilters filters);
        public Task<int> GetCardsCount(CardTariffsFilters filters);
        public Task<CardTariffsDto?> GetCardById(Guid cardId);
    }
}
