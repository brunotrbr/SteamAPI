using Microsoft.AspNetCore.Mvc.Filters;

namespace SteamAPI.Filters
{
    public class CustomActionFilterGlobal : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Executado Global Out");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Executado Global In");
        }
    }
}
