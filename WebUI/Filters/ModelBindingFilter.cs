using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebUI.Filters
{
    public class ModelBindingFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (context.Controller is Controller controller)
            {
                if (!controller.ModelState.IsValid)
                {
                    controller.ViewBag.Message = "Error!";
                    List<string> errors= new List<string>();
                    foreach(var error in controller.ModelState)
                    {
                        errors.Add(error.ToString());
                    }

                }
                else
                {
                    controller.ViewBag.Message = "Success!";
                }

            }
            await next();  
        }
    }
}
