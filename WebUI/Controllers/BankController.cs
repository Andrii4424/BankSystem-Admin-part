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
            return View(await _bankReadService.GetLimitedBanksListAsync(0, 6, null));
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [Route("/banks/{loadPageCount:int?}/{orderMethod?}")]
        public async Task<IActionResult> BanksList([FromRoute] int? loadPageCount, [FromRoute] string? orderMethod)
        {
            ViewBag.ModelCount = await _bankReadService.GetBanksCountAsync();
            int loadCount = loadPageCount.HasValue? loadPageCount.Value: 0;
            return View(await _bankReadService.GetLimitedBanksListAsync(0, loadCount, null));
        }

        [Route("/bank/{bankId:Guid}")]
        public async Task<IActionResult> Bank(Guid bankId)
        {
            return View(await _bankReadService.GetBankByIdAsync(bankId));
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [HttpGet("/add-bank")]
        public IActionResult AddBank([FromQuery] int? loadPageCount)
        {
            return View(new BankDto());
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [TypeFilter(typeof(ModelBindingFilter))]
        [HttpPost("/add-bank")]
        public IActionResult AddBank([FromForm] BankDto bankDto, [FromForm] int? loadPageCount)
        {
            if(ModelState.IsValid) _bankAddService.AddBankAsync(bankDto);
            return View(bankDto);
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [HttpGet("/update-bank/{bankId:Guid}")]
        public async Task<IActionResult> UpdateBank(Guid bankId, [FromQuery] int? loadPageCount)
        {
            return View(await _bankReadService.GetBankByIdAsync(bankId));
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [TypeFilter(typeof(ModelBindingFilter))]
        [HttpPost("/update-bank/{bankId:Guid}")]
        public async Task<IActionResult> UpdateBank([FromForm] BankDto bankDto, [FromRoute] Guid bankId, [FromForm] int? loadPageCount)
        {
            if (ModelState.IsValid) await _bankUpdateService.UpdateBankAsync(bankId, bankDto);
            return View(bankDto);
        }

        [HttpPost("/delete-bank/bank-id/{bankId:guid}/first-element/{firstElement:int}/order-method/{orderMethod?}")]
        public async Task<IActionResult> DeleteBank([FromRoute] Guid bankId, [FromRoute] int firstElement,[FromRoute] string? orderMethod)
        {
            await _bankDeleteService.DeleteBankAsync(bankId);
            return PartialView("_LoadBanks", await _bankReadService.GetLimitedBanksListAsync(firstElement-1, 1, orderMethod));
        }

        [HttpGet("/load-banks/{firstElement:int}/{elementsToLoad:int}/{orderMethod?}")]
        public async Task<IActionResult> LoadBanks([FromRoute] int firstElement,[FromRoute] int elementsToLoad, [FromRoute] string? orderMethod)
        {
            return PartialView("_LoadBanks", await _bankReadService.GetLimitedBanksListAsync(firstElement, elementsToLoad, orderMethod));
        }
    }
}
