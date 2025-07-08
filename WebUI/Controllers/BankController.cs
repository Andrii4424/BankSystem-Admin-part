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
            return View(await _bankReadService.GetLimitedBanksList(0, 6));
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [Route("/banks/{loadPageCount:int?}")]
        public async Task<IActionResult> BanksList([FromRoute] int? loadPageCount)
        {
            int loadCount = loadPageCount.HasValue? loadPageCount.Value: 0;
            return View(await _bankReadService.GetLimitedBanksList(0, loadCount));
        }

        [Route("/bank/{bankId:Guid}")]
        public async Task<IActionResult> Bank(Guid bankId)
        {
            return View(await _bankReadService.GetBankById(bankId));
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
            if(ModelState.IsValid) _bankAddService.AddBank(bankDto);
            return View(bankDto);
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [HttpGet("/update-bank/{bankId:Guid}")]
        public async Task<IActionResult> UpdateBank(Guid bankId, [FromQuery] int? loadPageCount)
        {
            return View(await _bankReadService.GetBankById(bankId));
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [TypeFilter(typeof(ModelBindingFilter))]
        [HttpPost("/update-bank/{bankId:Guid}")]
        public async Task<IActionResult> UpdateBank([FromForm] BankDto bankDto, [FromRoute] Guid bankId, [FromForm] int? loadPageCount)
        {
            if (ModelState.IsValid) await _bankUpdateService.UpdateBank(bankId, bankDto);
            return View(bankDto);
        }

        [HttpPost("/delete-bank/bankId/{bankId:guid}/firstElement/{firstElement:int}")]
        public async Task<IActionResult> DeleteBank([FromRoute] Guid bankId, [FromRoute] int firstElement)
        {
            await _bankDeleteService.DeleteBank(bankId);
            return PartialView("_LoadBanks", await _bankReadService.GetLimitedBanksList(firstElement-1, 1));
        }

        [HttpGet("/load-banks/{firstElement:int}")]
        public async Task<IActionResult> LoadBanks([FromRoute] int firstElement)
        {
            return View("_LoadBanks", await _bankReadService.GetLimitedBanksList(firstElement, 6));
        }
    }
}
