using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PrimeCafeManagement.Models;

namespace PrimeCafeManagement
{
    public class AuthorizeManagerAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string accessToken = context.HttpContext.Request.Cookies["user-access-token"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                PrimeCafeContext _context = context.HttpContext.RequestServices.GetRequiredService<PrimeCafeContext>();
                User manager = _context.Users.Where(x => x.AccessToken == accessToken && x.Role.Name == "Manager").FirstOrDefault();
                if (manager == null)
                {
                    context.Result = new RedirectResult("/Account/Login");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Account/Login");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }


    }
}
