using Application.DTO.BankProductDto;
using Application.DTO.FiltersDto;
using Application.ServiceContracts.ICardTarrifsService;
using AutoMapper;
using Domain.Entities.Banks;
using Domain.RepositoryContracts;
using System.Collections.Generic;

namespace Application.Services.CardTarrifsService
{
    public class CardTarrifsReadService: ICardTarrifsReadService
    {
        private readonly ICardTarrifsRepository _cardRepository;
        private readonly IBankRepository _bankRepository;
        private readonly ILogger<CardTarrifsReadService> _logger;
        private readonly IMapper _mapper;

        public CardTarrifsReadService(ICardTarrifsRepository cardRepository, ILogger<CardTarrifsReadService> logger, IMapper mapper, IBankRepository bankRepository)
        {
            _cardRepository = cardRepository;
            _logger = logger;
            _mapper = mapper;
            _bankRepository = bankRepository;
        }

        public async Task<List<CardTariffsDto>?> GetCardsAsync(CardTariffsFilters filters)
        {
            _logger.LogInformation("Getting card tariffs list by id");
            Filters<CardTariffsEntity> processedFilters = filters.ToGeneralFilters();
            List<CardTariffsEntity> cards = await _cardRepository.GetLimitedAsync(processedFilters.FirstItem, processedFilters.ElementsToLoad, processedFilters.SearchFilter,
                processedFilters.Ascending, processedFilters.SortValue, processedFilters.EntityFilters);
            List<CardTariffsDto>? cardDtos = _mapper.Map<List<CardTariffsDto>>(cards);
            List<BankEntity>? banks= await _bankRepository.GetAllValuesAsync() as List<BankEntity>;

            //Returns DTOs with bank name
            return GeneralServiceMethods<CardTariffsDto>.ToListWithBankName(cardDtos, banks);

        }

        public async Task<CardTariffsDto?> GetCardById(Guid cardId)
        {
            _logger.LogInformation("Trying get card tariffs by id {cardId}", cardId);
            CardTariffsEntity ? card = await _cardRepository.GetValueByIdAsync(cardId);
            if(card==null)
            {
                _logger.LogError("Card with id {cardId} doesnt exist", cardId);
                throw new ArgumentException($"Card with id {cardId} doesnt exist");
            }
            _logger.LogInformation("Successfull getting card by id {cardId}", cardId);
            return _mapper.Map<CardTariffsDto>(card);
        }

        public async Task<int> GetCardsCount(CardTariffsFilters filters)
        {
            Filters<CardTariffsEntity> processedFilters = filters.ToGeneralFilters();
            return await _cardRepository.CountAsync(processedFilters.SearchFilter, processedFilters.EntityFilters);
        }
    }
}
