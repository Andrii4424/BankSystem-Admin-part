using Application.DTO.BankProductDto;
using Application.ServiceContracts.BankServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Filters;

namespace WebUI.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankReadService _bankReadService;
        private readonly IBankAddService _bankAddService;
        private readonly IBankUpdateService _bankUpdateService;
        private readonly IBankDeleteService _bankDeleteService;

        public BankController(IBankReadService bankReadService, IBankAddService bankAddService, IBankUpdateService bankUpdateService,
            IBankDeleteService bankDeleteService)
        {
            _bankReadService = bankReadService;
            _bankAddService = bankAddService;
            _bankUpdateService = bankUpdateService;
            _bankDeleteService = bankDeleteService;
        }

        [Route("/banks")]
        public async Task<IActionResult> BanksList()
        {
            ViewBag.ModelCount = await _bankReadService.GetBanksCountAsync();
            return View(await _bankReadService.GetLimitedBanksListAsync(0, 6, null, null, null, null, null, null, null));
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [Route("/banks/{loadPageCount:int?}/start-method/{orderMethod?}")]
        public async Task<IActionResult> BanksList([FromRoute] int? loadPageCount, [FromRoute] string? orderMethod)
        {
            ViewBag.ModelCount = await _bankReadService.GetBanksCountAsync();
            int loadCount = loadPageCount.HasValue? loadPageCount.Value: 0;
            ViewBag.OrderMethod = orderMethod;
            return View(await _bankReadService.GetLimitedBanksListAsync(0, loadCount, null, orderMethod, null, null, null, null, null));
        }

        [Route("/bank/{bankId:Guid}")]
        public async Task<IActionResult> Bank(Guid bankId)
        {
            return View(await _bankReadService.GetBankByIdAsync(bankId));
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [HttpGet("/add-bank")]
        public IActionResult AddBank([FromQuery] int? loadPageCount, [FromQuery] string? orderMethod)
        {
            return View(new BankDto());
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [TypeFilter(typeof(ModelBindingFilter))]
        [HttpPost("/add-bank")]
        public IActionResult AddBank([FromForm] BankDto bankDto, [FromForm] int? loadPageCount, [FromForm] string? orderMethod)
        {
            if(ModelState.IsValid) _bankAddService.AddBankAsync(bankDto);
            return View(bankDto);
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [HttpGet("/update-bank/{bankId:Guid}")]
        public async Task<IActionResult> UpdateBank(Guid bankId, [FromQuery] int? loadPageCount, [FromQuery] string? orderMethod)
        {
            return View(await _bankReadService.GetBankByIdAsync(bankId));
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [TypeFilter(typeof(ModelBindingFilter))]
        [HttpPost("/update-bank/{bankId:Guid}")]
        public async Task<IActionResult> UpdateBank([FromForm] BankDto bankDto, [FromRoute] Guid bankId, [FromForm] int? loadPageCount,
            [FromForm] string? orderMethod)
        {
            if (ModelState.IsValid) await _bankUpdateService.UpdateBankAsync(bankId, bankDto);
            return View(bankDto);
        }

        [HttpPost("/delete-bank/bank-id/{bankId:guid}/first-element/{firstElement:int}/order-method/{orderMethod?}")]
        public async Task<IActionResult> DeleteBank([FromRoute] Guid bankId, [FromRoute] int firstElement,[FromRoute] string? orderMethod)
        {
            await _bankDeleteService.DeleteBankAsync(bankId);
            return PartialView("_LoadBanks", await _bankReadService.GetLimitedBanksListAsync(firstElement-1, 1,null, orderMethod, null, null, 
                null, null, null));
        }

        [HttpGet("/load-banks/{firstElement:int}/{elementsToLoad:int}/{searchValue?}/{orderMethod:?}/{licenseFilter:bool?}/{siteFilter:bool?}/{ratingFilter:double?}/{clientsCountFilter:int?}/{capitalizationFilter:int?}")]
        public async Task<IActionResult> LoadBanks([FromRoute] int firstElement, [FromRoute] int elementsToLoad, [FromRoute] string? searchValue,
            [FromRoute] string? orderMethod, [FromRoute] bool? licenseFilter, [FromRoute] bool? siteFilter, [FromRoute] double? ratingFilter,
            [FromRoute] int? clientsCountFilter, [FromRoute] int? capitalizationFilter)
        {
            return PartialView("_LoadBanks", await _bankReadService.GetLimitedBanksListAsync(firstElement, elementsToLoad, searchValue, orderMethod,
                licenseFilter, siteFilter, ratingFilter, clientsCountFilter, capitalizationFilter));
        }
    }
}
