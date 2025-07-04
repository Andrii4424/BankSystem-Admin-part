using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                    foreach(var value in controller.ModelState.Values)
                    {
                        foreach(var error in value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                    controller.ViewBag.Errors = errors;
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
