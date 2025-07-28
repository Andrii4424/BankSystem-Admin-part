using Application.DTO.BankProductDto;
using Application.DTO.FiltersDto;
using Application.ServiceContracts.BankServiceContracts;
using Application.ServiceContracts.ICardTarrifsService;
using Microsoft.AspNetCore.Mvc;
using WebUI.Filters;

namespace WebUI.Controllers
{
    public class CardTariffsController : Controller
    {
        private readonly ICardTarrifsReadService _cardTarrifsReadService;
        private readonly ICardTarrifsAddService _cardTarrifsAddService;
        private readonly ICardTarrifsUpdateService _cardTarrifsUpdateService;
        private readonly ICardTarrifsDeleteService _cardTarrifsDeleteService;
        private readonly IBankReadService _bankReadService;

        public CardTariffsController(ICardTarrifsReadService cardTarrifsReadService, ICardTarrifsAddService cardTarrifsAddService,
            ICardTarrifsUpdateService cardTarrifsUpdateService, ICardTarrifsDeleteService cardTarrifsDeleteService, IBankReadService bankReadService)
        {
            _cardTarrifsReadService = cardTarrifsReadService;
            _cardTarrifsAddService = cardTarrifsAddService;
            _cardTarrifsUpdateService = cardTarrifsUpdateService;
            _cardTarrifsDeleteService = cardTarrifsDeleteService;
            _bankReadService = bankReadService;
        }

        [Route("/cards")]
        public async Task<IActionResult> CardTariffsList()
        {
            return View(await _cardTarrifsReadService.GetCardsAsync(new CardTariffsFilters()));
        }
        //Add Actions
        [HttpGet("/add-card-tariffs/bank-id/{bankId:guid}")]
        public async Task<IActionResult> AddCard([FromRoute] Guid bankId)
        {
            return View(new CardTariffsDto());
        }

        [TypeFilter(typeof(ModelBindingFilter))]
        [HttpPost("/add-card/bank-id/{bankId:guid}")]
        public async Task<IActionResult> AddCard([FromRoute] Guid bankId, [FromForm] CardTariffsDto cardDto)
        {
            cardDto.BankId = bankId;
            await _cardTarrifsAddService.AddCardAsync(cardDto);
            return View();
        }
    }
}
