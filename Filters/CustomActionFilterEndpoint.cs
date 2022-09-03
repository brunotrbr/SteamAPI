using Microsoft.AspNetCore.Mvc.Filters;

namespace SteamAPI.Filters
{
    public class CustomActionFilterEndpoint : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Console.WriteLine("Action Filter no Endpoint, executado depois da chamada do método (OnActionExecuted)");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Console.WriteLine("Action Filter no Endpoint, executado antes da chamada do método (OnActionExecuting)");
        }
    }
}
