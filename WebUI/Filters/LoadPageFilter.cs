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
                if(context.ActionArguments.ContainsKey("loadPageCount") && context.ActionArguments["loadPageCount"] != null)
                {
                    controller.ViewBag.StartCount = Convert.ToInt32(context.ActionArguments["loadPageCount"]);
                    controller.ViewBag.OrderMethod = context.ActionArguments["orderMethod"];
                }
            }
            await next();
        }
    }
}
