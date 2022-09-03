using Microsoft.AspNetCore.Mvc.Filters;

namespace SteamAPI.Filters
{
    public class CustomAsyncActionFilterController : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Console.WriteLine("Action Filter na Controller, executado antes da chamada do método (antes do next)");

            await next();

            //Console.WriteLine("Action Filter na Controller, executado depois da chamada do método (depois do next)");
        }
    }
}
