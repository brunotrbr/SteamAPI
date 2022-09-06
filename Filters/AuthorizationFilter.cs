using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace SteamAPI.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            StringValues authToken;
            if(context.HttpContext.Request.Headers.TryGetValue("authToken", out authToken) == false || authToken.Equals("123") == false)
            {
                context.Result = new ObjectResult(new
                {
                    message = "Acesso não autorizado"
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }

            //if (context.HttpContext.Request.Headers.ContainsKey("authToken") == false)
            //{
            //    context.Result = new ObjectResult(new
            //    {
            //        message = "Acesso não autorizado"
            //    })
            //    {
            //        StatusCode = StatusCodes.Status403Forbidden
            //    };
            //}

            //var authToken = context.HttpContext.Request.Headers["autToken"];
            //if(authToken.Equals("123") == false)
            //{
            //    context.Result = new ObjectResult(new
            //    {
            //        message = "Acesso não autorizado"
            //    })
            //    {
            //        StatusCode = StatusCodes.Status403Forbidden
            //    };
            //}
        }
    }
}
