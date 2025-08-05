using Application.DTO;
using Application.DTO.BankProductDto;
using Application.DTO.FiltersDto;
using Application.ServiceContracts.BankServiceContracts;
using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;
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
            Response.Cookies.Append("returnActionUrl", "bankList");
            ViewBag.ModelCount = await _bankReadService.GetBanksCountAsync(null, null, null, null, null, null);
            return View(await _bankReadService.GetLimitedBanksListAsync(0, 6, null, null, null, null, null, null, null));
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [Route("/banks/{elementsToLoad:int?}/{searchValue?}/{orderMethod:?}/{licenseFilter:bool?}/{siteFilter:bool?}/{ratingFilter:double?}/{clientsCountFilter:int?}/{capitalizationFilter:int?}")]
        public async Task<IActionResult> BanksListFiltered( [FromRoute] int? elementsToLoad, [FromRoute] string? searchValue,
            [FromRoute] string? orderMethod, [FromRoute] bool? licenseFilter, [FromRoute] bool? siteFilter, [FromRoute] double? ratingFilter,
            [FromRoute] int? clientsCountFilter, [FromRoute] int? capitalizationFilter)
        {
            ViewBag.ModelCount = await _bankReadService.GetBanksCountAsync(searchValue, licenseFilter, siteFilter, ratingFilter, clientsCountFilter, capitalizationFilter);
            int loadCount = elementsToLoad.HasValue? elementsToLoad.Value: 0;
            ViewBag.OrderMethod = orderMethod;
            return View("BanksList", await _bankReadService.GetLimitedBanksListAsync(0, loadCount, searchValue, orderMethod, licenseFilter, 
                siteFilter, ratingFilter, clientsCountFilter, capitalizationFilter));
        }

        [Route("/bank/{bankId:Guid}")]
        public async Task<IActionResult> BankInfo(Guid bankId)
        {
            ViewBag.ReturnUrl = Request.Cookies["returnActionUrl"];
            Response.Cookies.Append("returnUrl", "Bank");
            return View(await _bankReadService.GetBankByIdAsync(bankId));
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [HttpGet("/add-bank")]
        public IActionResult AddBank([FromQuery] int? elementsToLoad, [FromQuery] string? orderMethod, [FromQuery] string? searchValue, 
            [FromQuery] bool? licenseFilter, [FromQuery] bool? siteFilter, [FromQuery] double? ratingFilter, 
            [FromQuery] int? clientsCountFilter, [FromQuery] int? capitalizationFilter)
        {
            return View(new BankDto());
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [TypeFilter(typeof(ModelBindingFilter))]
        [HttpPost("/add-bank")]
        public async Task<IActionResult> AddBank([FromForm] BankDto bankDto, [FromForm] IFormFile? bankLogo, [FromForm] int? elementsToLoad, 
            [FromForm] string? orderMethod, [FromForm] string? searchValue,[FromForm] bool? licenseFilter, [FromForm] bool? siteFilter, 
            [FromForm] double? ratingFilter, [FromForm] int? clientsCountFilter, [FromForm] int? capitalizationFilter)
        {
            if (ModelState.IsValid)
            {
                OperationResult result = await _bankAddService.AddBankAsync(bankDto, bankLogo);
                if (!result.Success)
                {
                    ViewBag.Message = "Error!";
                    List<string> errors = new List<string>() { result.ErrorMessage };
                    ViewBag.Errors=errors;
                }
            }
            ViewBag.StartCount = ((elementsToLoad%6!=0 || elementsToLoad==0) && _bankReadService.IsObjectMatchesFilters(bankDto, searchValue, 
                licenseFilter, siteFilter, ratingFilter, clientsCountFilter, capitalizationFilter))? elementsToLoad+1: elementsToLoad; 
            return View(bankDto);
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [HttpGet("/update-bank/{bankId:Guid}")]
        public async Task<IActionResult> UpdateBank([FromRoute] Guid bankId, [FromQuery] int? elementsToLoad, [FromQuery] string? orderMethod, 
            [FromQuery] string? searchValue,[FromQuery] bool? licenseFilter, [FromQuery] bool? siteFilter, [FromQuery] double? ratingFilter,
            [FromQuery] int? clientsCountFilter, [FromQuery] int? capitalizationFilter, [FromQuery] bool? fromBankInfo)
        {
            ViewBag.FromBankInfo = fromBankInfo;
            return View(await _bankReadService.GetBankByIdAsync(bankId));
        }

        [TypeFilter(typeof(LoadPageFilter))]
        [TypeFilter(typeof(ModelBindingFilter))]
        [HttpPost("/update-bank/{bankId:Guid}")]
        public async Task<IActionResult> UpdateBank([FromForm] BankDto bankDto, [FromForm] IFormFile? bankLogo, [FromRoute] Guid bankId, 
            [FromForm] int? elementsToLoad, [FromForm] string? orderMethod, [FromForm] string? searchValue, [FromForm] bool? licenseFilter, 
            [FromForm] bool? siteFilter, [FromForm] double? ratingFilter, [FromForm] int? clientsCountFilter, [FromForm] int? capitalizationFilter,
            [FromQuery] bool? fromBankInfo)
        {
            ViewBag.FromBankInfo = fromBankInfo;
            if (ModelState.IsValid)
            {
                OperationResult result = await _bankUpdateService.UpdateBankAsync(bankId, bankDto, bankLogo);
                if (!result.Success)
                {
                    ViewBag.Message = "Error!";
                    List<string> errors = new List<string>() { result.ErrorMessage };
                    ViewBag.Errors = errors;
                }
                bankDto.Id=bankId;
            }
            return View(bankDto);
        }

        [HttpPost("/delete-bank/bank-id/{bankId:guid}")]
        public async Task<IActionResult> DeleteBank([FromRoute] Guid bankId)
        {
            await _bankDeleteService.DeleteBankAsync(bankId);
            return RedirectToAction("BanksList");
        }

        [HttpPost("/delete-bank/bank-id/{bankId:guid}/first-element/{firstElement:int}/{searchValue?}/{orderMethod:?}/{licenseFilter:bool?}/{siteFilter:bool?}/{ratingFilter:double?}/{clientsCountFilter:int?}/{capitalizationFilter:int?}")]
        public async Task<IActionResult> DeleteBank([FromRoute] Guid bankId, [FromRoute] int firstElement, [FromRoute] string? searchValue,
            [FromRoute] string? orderMethod, [FromRoute] bool? licenseFilter, [FromRoute] bool? siteFilter, [FromRoute] double? ratingFilter,
            [FromRoute] int? clientsCountFilter, [FromRoute] int? capitalizationFilter)
        {
            await _bankDeleteService.DeleteBankAsync(bankId);
            ViewBag.ElementsCount = await _bankReadService.GetBanksCountAsync(searchValue, licenseFilter, siteFilter, ratingFilter, clientsCountFilter, capitalizationFilter);
            return PartialView("_LoadBanks", await _bankReadService.GetLimitedBanksListAsync(firstElement-1, 1, searchValue, orderMethod,
                licenseFilter, siteFilter, ratingFilter, clientsCountFilter, capitalizationFilter));
        }

        [HttpGet("/load-banks/{firstElement:int}/{elementsToLoad:int}/{searchValue?}/{orderMethod:?}/{licenseFilter:bool?}/{siteFilter:bool?}/{ratingFilter:double?}/{clientsCountFilter:int?}/{capitalizationFilter:int?}")]
        public async Task<IActionResult> LoadBanks([FromRoute] int firstElement, [FromRoute] int elementsToLoad, [FromRoute] string? searchValue,
            [FromRoute] string? orderMethod, [FromRoute] bool? licenseFilter, [FromRoute] bool? siteFilter, [FromRoute] double? ratingFilter,
            [FromRoute] int? clientsCountFilter, [FromRoute] int? capitalizationFilter)
        {
            ViewBag.SearchFilter = (searchValue==null || searchValue.Trim()=="0")? null: searchValue.Trim();
            ViewBag.ElementsCount =await _bankReadService.GetBanksCountAsync(searchValue, licenseFilter, siteFilter, ratingFilter, clientsCountFilter, capitalizationFilter);
            return PartialView("_LoadBanks", await _bankReadService.GetLimitedBanksListAsync(firstElement, elementsToLoad, searchValue, orderMethod,
                licenseFilter, siteFilter, ratingFilter, clientsCountFilter, capitalizationFilter));
        }

        //Method to load banks when user want to add another entity
        [HttpGet("/get-banks/for/{entity}")]
        public async Task<IActionResult> GetBanksForChose([FromRoute] string entity)
        {
            ViewBag.ReturnActionUrl = Request.Cookies["returnActionUrl"];
            ViewBag.Entity = entity;
            ViewBag.Count = await _bankReadService.GetCountByDtoFilter(new BankFilters());
            return PartialView("ChoseBank", await _bankReadService.GetLimitedByDtoFilterAsync(new BankFilters()));
        }

        [HttpPost("/load-banks/for/{entity}")]
        public async Task<IActionResult> LoadBanksForChose([FromForm] BankFilters bankFilters, [FromRoute] string entity)
        {
            ViewBag.Entity = entity;
            ViewBag.Count = await _bankReadService.GetCountByDtoFilter(bankFilters);
            return PartialView("_GetBanksToChose", await _bankReadService.GetLimitedByDtoFilterAsync(bankFilters));
        }
    }
}
