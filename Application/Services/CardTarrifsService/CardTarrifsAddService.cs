using Application.DTO;
using Application.DTO.BankProductDto;
using Application.ServiceContracts.ICardTarrifsService;
using AutoMapper;
using Domain.Entities.Banks;
using Domain.RepositoryContracts;

namespace Application.Services.CardTarrifsService
{
    public class CardTarrifsAddService: ICardTarrifsAddService
    {
        private readonly ICardTarrifsRepository _cardRepository;
        private readonly IBankRepository _bankRepository;
        private readonly ILogger<CardTarrifsAddService> _logger;
        private readonly IMapper _mapper;

        public CardTarrifsAddService(ICardTarrifsRepository cardRepository, IBankRepository bankRepository,
            ILogger<CardTarrifsAddService> logger, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _bankRepository = bankRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<OperationResult> AddCardAsync(CardTariffsDto cardDto)
        {
            _logger.LogInformation("Attempting to add card tariffs with name {CardName}", cardDto.CardName);
            if (!await _bankRepository.IsObjectIdExists(cardDto.BankId)) {
                _logger.LogError("Trying add card to non-existent bank, bank with Id {BankId} doesnt exist", cardDto.BankId);
                throw new ArgumentException($"Trying add card to non-existent bank, bank with Id {cardDto.BankId} doesnt exist");
            }
            else if (await _cardRepository.IsExists(c => c.BankId == cardDto.BankId && c.CardName.ToUpper() == cardDto.CardName.ToUpper())) {
                _logger.LogWarning("Unsuccessfull add card attemp: card with name {cardName} already exists for this bank {bankId}", cardDto.CardName, cardDto.BankId);
                return OperationResult.Error("A card with this name already exists for this bank. Please change card name and try again");
            }
            await _cardRepository.AddAsync(_mapper.Map<CardTariffsEntity>(cardDto));
            await _cardRepository.SaveAsync();
            _logger.LogInformation("Card with name {CardName} has been successfully added to bank with id {BankId}", cardDto.CardName, cardDto.BankId);

            return OperationResult.Ok();
        }
    }
}
