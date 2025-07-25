using Application.DTO.BankProductDto;
using Application.DTO.FiltersDto;
using Application.ServiceContracts.ICardTarrifsService;
using AutoMapper;
using Domain.Entities.Banks;
using Domain.RepositoryContracts;

namespace Application.Services.CardTarrifsService
{
    public class CardTarrifsReadService: ICardTarrifsReadService
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

        public async Task<List<CardTariffsDto>?> GetCardsAsync(CardTariffsFilters filters)
        {
            Filters<CardTariffsEntity> processedFilters = filters.ToGeneralFilters();
            List<CardTariffsEntity> cards = await _cardRepository.GetLimitedAsync(processedFilters.FirstItem, processedFilters.ElementsToLoad, processedFilters.SearchFilter,
                processedFilters.Ascending, processedFilters.SortValue, processedFilters.EntityFilters);
            return _mapper.Map<List<CardTariffsDto>>(cards);
        }
    }
}
