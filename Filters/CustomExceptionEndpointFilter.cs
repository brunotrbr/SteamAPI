using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SteamAPI.Filters
{
    public class CustomExceptionEndpointFilter : Attribute,IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //context.Result = new ObjectResult(new
            //{
            //    message = "Ops! Ocorreu um erro inesperado. Filtro de endpoint"
            //})
            //{
            //    StatusCode = StatusCodes.Status500InternalServerError
            //};

            //context.ExceptionHandled = false;
        }
    }
}
