using Microsoft.AspNetCore.Mvc.Filters;

namespace SteamAPI.Filters
{
    public class CustomActionFilterEndpoint : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Executado depois da chamada do método (OnActionExecuted)");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Executado antes da chamada do método (OnActionExecuting)");
        }
    }
}
