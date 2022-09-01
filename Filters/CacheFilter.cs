using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SteamAPI.Models;
using System.Text.Json;

namespace SteamAPI.Filters
{
    public class ShortCircuitFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var before = context;
            // Imaginem que esta linha abaixo está recuperando os dados do cache
            // Neste caso, o fluxo de execução do pipeline de filtros é interrompido (short-circuit)
            var res = JsonSerializer.Serialize(new Games(1, 1, "Cache", "a", "a", "a", "a"));
            context.Result = new ContentResult()
            {
                Content = res
            };
        }
    }

    
}
