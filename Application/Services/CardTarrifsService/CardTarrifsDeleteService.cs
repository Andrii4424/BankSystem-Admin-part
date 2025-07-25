using Application.ServiceContracts.ICardTarrifsService;
using AutoMapper;
using Domain.Entities.Banks;
using Domain.RepositoryContracts;

namespace Application.Services.CardTarrifsService
{
    public class CardTarrifsDeleteService : ICardTarrifsDeleteService
    {
        private readonly ICardTarrifsRepository _cardRepository;
        private readonly ILogger<CardTarrifsDeleteService> _logger;
        private readonly IMapper _mapper;

        public CardTarrifsDeleteService(ICardTarrifsRepository cardRepository, ILogger<CardTarrifsDeleteService> logger, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task DeleteCardAsync(Guid cardId)
        {
            CardTariffsEntity? card = await _cardRepository.GetValueByIdAsync(cardId);
            if (card != null)
            {
                _cardRepository.DeleteElement(card);
                await _cardRepository.SaveAsync();
            }

        }

    }
}
