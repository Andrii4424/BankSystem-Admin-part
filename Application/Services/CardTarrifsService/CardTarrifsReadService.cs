using Application.DTO.BankProductDto;
using Application.DTO.FiltersDto;
using AutoMapper;
using Domain.RepositoryContracts;

namespace Application.Services.CardTarrifsService
{
    public class CardTarrifsReadService
    {
        private readonly ICardTarrifsRepository _cardRepository;
        private readonly ILogger<CardTarrifsReadService> _logger;
        private readonly IMapper _mapper;

        public CardTarrifsReadService(ICardTarrifsRepository cardRepository, ILogger<CardTarrifsReadService> logger, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /*public async Task<List<CardTariffsDto>?> GetCards(CardTariffsFilters filters)
        {

        }*/

    }
}
