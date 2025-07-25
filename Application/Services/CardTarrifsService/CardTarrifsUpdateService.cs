using Application.DTO;
using Application.DTO.BankProductDto;
using Application.ServiceContracts.ICardTarrifsService;
using AutoMapper;
using Domain.Entities.Banks;
using Domain.RepositoryContracts;

namespace Application.Services.CardTarrifsService
{
    public class CardTarrifsUpdateService : ICardTarrifsUpdateService
    {
        private readonly ICardTarrifsRepository _cardRepository;
        private readonly IBankRepository _bankRepository;
        private readonly ILogger<CardTarrifsUpdateService> _logger;
        private readonly IMapper _mapper;

        public CardTarrifsUpdateService(ICardTarrifsRepository cardRepository, IBankRepository bankRepository,
            ILogger<CardTarrifsUpdateService> logger, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _bankRepository = bankRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<OperationResult> UpdateCardAsync(CardTariffsDto cardDto)
        {
            _logger.LogInformation("Attempting to add update card tariffs with name {CardName}", cardDto.CardName);
            if(!await _cardRepository.IsObjectIdExists(cardDto.Id))
            {
                _logger.LogError("Trying update non-existent card with id {cardId}", cardDto.Id);
                throw new ArgumentException($"Trying update non-existent card with id {cardDto.Id}");
            }
            else if (!await _bankRepository.IsObjectIdExists(cardDto.BankId))
            {
                _logger.LogError("Trying update card to non-existent bank, bank with Id {BankId} doesnt exist", cardDto.BankId);
                throw new ArgumentException($"Trying update card to non-existent bank, bank with Id {cardDto.BankId} doesnt exist");
            }

            _cardRepository.UpdateObject(_mapper.Map<CardTariffsEntity>(cardDto));
            await _cardRepository.SaveAsync();
            _logger.LogInformation("Card with name {CardName} has been successfully updated", cardDto.CardName);

            return OperationResult.Ok();
        }

    }
}
