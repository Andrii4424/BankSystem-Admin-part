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
            return View(await _bankReadService.GetBanksList());
        }

        [Route("/bank/{bankId:Guid}")]
        public async Task<IActionResult> BanksList(Guid bankId)
        {
            return View(await _bankReadService.GetBankById(bankId));
        }

        [HttpGet("/add-bank")]
        public IActionResult AddBank()
        {
            return View(new BankDto());
        }

        [TypeFilter(typeof(ModelBindingFilter))]
        [HttpPost("/add-bank")]
        public IActionResult AddBank([FromForm] BankDto bankDto)
        {
            if(ModelState.IsValid) _bankAddService.AddBank(bankDto);
            return View(bankDto);
        }

        [HttpGet("/update-bank/{bankId:Guid}")]
        public async Task<IActionResult> UpdateBank(Guid bankId)
        {
            return View(await _bankReadService.GetBankById(bankId));
        }

        [TypeFilter(typeof(ModelBindingFilter))]
        [HttpPost("/update-bank/{bankId:Guid}")]
        public async Task<IActionResult> UpdateBank([FromForm] BankDto bankDto, [FromRoute] Guid bankId)
        {
            if (ModelState.IsValid) await _bankUpdateService.UpdateBank(bankId, bankDto);
            return View(bankDto);
        }

        [HttpPost("/delete-bank/{bankId:Guid}")]
        public async Task<IActionResult> DeleteBank([FromRoute] Guid bankId)
        {
            await _bankDeleteService.DeleteBank(bankId);
            return RedirectToAction("BanksList");
        }

        [HttpGet("/load-banks/{elementsCount:int}")]
        public async Task<IActionResult> LoadBanks([FromRoute] int elementsCount)
        {
            return View("_LoadBanks", await _bankReadService.GetLimitedBanksList(elementsCount));
        }
    }
}
