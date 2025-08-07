using Application.DTO;
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

        //Read
        [Route("/cards")]
        public async Task<IActionResult> CardTariffsList()
        {
            Response.Cookies.Append("returnUrl", "cardTariffs");
            Response.Cookies.Append("returnActionUrl", "cardTariffsList");
            ViewBag.Count = await _cardTarrifsReadService.GetCardsCount(new CardTariffsFilters());
            return View(await _cardTarrifsReadService.GetCardsAsync(new CardTariffsFilters()));
        }

        [HttpPost("/get-card-tariffs")]
        public async Task<IActionResult> LoadCards(CardTariffsFilters filters)
        {
            ViewBag.Count = await _cardTarrifsReadService.GetCardsCount(filters);
            return PartialView("_LoadCardTariffs", await _cardTarrifsReadService.GetCardsAsync(filters));
        }

        [Route("card-tariffs-info/{cardId:guid}")]
        public async Task<IActionResult> CardTariffsInfo(Guid cardId)
        {
            Response.Cookies.Append("returnActionUrl", "cardTariffsInfo");
            ViewBag.ReturnUrl = Request.Cookies["returnUrl"];
            return View(await _cardTarrifsReadService.GetCardById(cardId));
        }

        //Add Actions
        [HttpGet("/add-card-tariffs/bank-id/{bankId:guid}")]
        public async Task<IActionResult> AddCard([FromRoute] Guid bankId)
        {
            ViewBag.ReturnUrl = Request.Cookies["returnUrl"];
            ViewBag.BankId = bankId;
            ViewBag.BankName = await _bankReadService.GetBankNameById(bankId);
            return View(new CardTariffsDto());
        }

        [TypeFilter(typeof(ModelBindingFilter))]
        [HttpPost("/add-card-tariffs/bank-id/{bankId:guid}")]
        public async Task<IActionResult> AddCard([FromRoute] Guid bankId, [FromForm] CardTariffsDto cardDto)
        {
            ViewBag.ReturnUrl = Request.Cookies["returnUrl"];
            ViewBag.BankId = bankId;
            ViewBag.BankName = await _bankReadService.GetBankNameById(bankId);
            if (ModelState.IsValid)
            {
                OperationResult result = await _cardTarrifsAddService.AddCardAsync(cardDto);
                ViewBag.Message = "Success!";
                if (!result.Success) {
                    ViewBag.Message = "Error!";
                    List<string> errors = new List<string>();
                    errors.Add(result.ErrorMessage);
                    ViewBag.Errors = errors;
                }
            }
            return View(cardDto);
        }

        //Update 
        [HttpGet("/update-card-tariffs/card-id/{cardId:guid}")]
        public async Task<IActionResult> UpdateCard ([FromRoute] Guid cardId)
        {
            ViewBag.ReturnActionUrl = Request.Cookies["returnActionUrl"];
            CardTariffsDto? card = await _cardTarrifsReadService.GetCardById(cardId);
            ViewBag.BankName = await _bankReadService.GetBankNameById(card.BankId);
            return View(card);
        }

        [TypeFilter(typeof(ModelBindingFilter))]
        [HttpPost("/update-card-tariffs/card-id/{cardId:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid cardId, [FromForm] CardTariffsDto cardDto)
        {
            ViewBag.ReturnActionUrl = Request.Cookies["returnActionUrl"];
            ViewBag.BankName = await _bankReadService.GetBankNameById(cardDto.BankId);
            if (ModelState.IsValid)
            {
                OperationResult result = await _cardTarrifsUpdateService.UpdateCardAsync(cardId, cardDto);
                ViewBag.Message = "Success!";
                if (!result.Success)
                {
                    ViewBag.Message = "Error!";
                    List<string> errors = new List<string>();
                    errors.Add(result.ErrorMessage);
                    ViewBag.Errors = errors;
                }
            }
            return View(cardDto);
        }

        //Delete
        [HttpDelete("DeleteCard/{cardId}")]
        public async Task<IActionResult> DeleteCard(Guid cardId)
        {
            await _cardTarrifsDeleteService.DeleteCardAsync(cardId);
            return Ok();
        }

        [HttpPost("/delete-card-with-redirect/{cardId}")]
        public async Task<IActionResult> DeleteCardWithRedirect(Guid cardId)
        {
            Guid banksId= (await _cardTarrifsReadService.GetCardById(cardId)).BankId; 
            await _cardTarrifsDeleteService.DeleteCardAsync(cardId);
            if(Request.Cookies["returnUrl"]== "cardTariffs")
            {
                return RedirectToAction("CardTariffsList");
            }
            else
            {
                return RedirectToAction("Bank", "BankInfo", new {bankId= banksId });
            }
        }
    }
}
