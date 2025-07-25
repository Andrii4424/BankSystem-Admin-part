namespace Application.ServiceContracts.ICardTarrifsService
{
    public interface ICardTarrifsDeleteService
    {
        public Task DeleteCardAsync(Guid cardId);
    }
}
