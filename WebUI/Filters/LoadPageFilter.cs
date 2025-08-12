using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebUI.Filters
{
    public class LoadPageFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(context.Controller is Controller controller)
            {
                controller.ViewBag.FiltersValue = new Dictionary<string, string?>
                {
                    ["LicenseFilter"] = null,
                    ["SiteFilter"] = null,
                    ["RatingFilter"] = null,
                    ["ClientsCountFilter"] = null,
                    ["CapitalizationFilter"] = null
                };
                if (context.ActionArguments.ContainsKey("elementsToLoad") && context.ActionArguments["elementsToLoad"] != null)
                {
                    int elementsToLoad = Convert.ToInt32(context.ActionArguments["elementsToLoad"]);
                    if (elementsToLoad>=6)
                    {
                        controller.ViewBag.StartCount = elementsToLoad;
                    }
                    else
                    {
                        controller.ViewBag.StartCount = 6;
                    }
                    controller.ViewBag.OrderMethod = context.ActionArguments["orderMethod"];
                    controller.ViewBag.SearchFilter = context.ActionArguments["searchValue"];

                    controller.ViewBag.FiltersValue = new Dictionary<string, string?>
                    {
                        ["LicenseFilter"] = Convert.ToString(context.ActionArguments["licenseFilter"]),
                        ["SiteFilter"] = Convert.ToString(context.ActionArguments["siteFilter"]),
                        ["RatingFilter"] = Convert.ToString(context.ActionArguments["ratingFilter"]),
                        ["ClientsCountFilter"] = Convert.ToString(context.ActionArguments["clientsCountFilter"]),
                        ["CapitalizationFilter"] = Convert.ToString(context.ActionArguments["capitalizationFilter"])
                    };
                }
            }
            await next();
        }
    }
}
