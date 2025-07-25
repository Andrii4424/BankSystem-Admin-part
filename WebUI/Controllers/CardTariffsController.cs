using Application.ServiceContracts.ICardTarrifsService;
using Microsoft.AspNetCore.Mvc;
using Application.DTO.FiltersDto;

namespace WebUI.Controllers
{
    public class CardTariffsController : Controller
    {
        private readonly ICardTarrifsReadService _cardTarrifsReadService;
        private readonly ICardTarrifsAddService _cardTarrifsAddService;
        private readonly ICardTarrifsUpdateService _cardTarrifsUpdateService;
        private readonly ICardTarrifsDeleteService _cardTarrifsDeleteService;

        public CardTariffsController(ICardTarrifsReadService cardTarrifsReadService, ICardTarrifsAddService cardTarrifsAddService,
            ICardTarrifsUpdateService cardTarrifsUpdateService, ICardTarrifsDeleteService cardTarrifsDeleteService)
        {
            _cardTarrifsReadService = cardTarrifsReadService;
            _cardTarrifsAddService = cardTarrifsAddService;
            _cardTarrifsUpdateService = cardTarrifsUpdateService;
            _cardTarrifsDeleteService = cardTarrifsDeleteService;
        }


        [Route("/cards")]
        public async Task<IActionResult> CardTariffsList()
        {
            return View(await _cardTarrifsReadService.GetCardsAsync(new CardTariffsFilters()));
        }
    }
}
