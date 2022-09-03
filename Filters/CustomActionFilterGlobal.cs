using Microsoft.AspNetCore.Mvc.Filters;

namespace SteamAPI.Filters
{
    public class CustomActionFilterGlobal : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Console.WriteLine("Action Filter Global, executado depois da chamada do método (OnActionExecuted)");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Console.WriteLine("Action Filter Global, executado antes da chamada do método (OnActionExecuting)");
        }
    }
}
