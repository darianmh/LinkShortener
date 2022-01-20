using System.Threading.Tasks;
using Data.LinkShortener.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.LinkShortener.Services.User;

namespace Services.LinkShortener.Classes
{
    public class AdminFilterNameAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public string Name { get; }
        public string Group { get; }
        public string AccessName => $"{Name} - {Group}";
        public AdminFilterNameAttribute(AdminControllerNames group, string name)
        {
            Name = name;
            Group = group.ToString();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userManager = (ApplicationUserManager)context.HttpContext.RequestServices.GetService(typeof(ApplicationUserManager));
            if (userManager == null) return;
            var user = context.HttpContext.User;

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                var url = new
                {
                    ReturnUrl = $"{context.HttpContext.Request.GetDisplayUrl()}"
                };
                context.Result = new RedirectToActionResult("Login", "Account", url);
                return;
            }

            var hasAccess =
                await userManager.UserHasAccess(user.Identity.Name, AccessName);
            if (!hasAccess)
            {
                var url = new
                {
                    ReturnUrl = $"{context.HttpContext.Request.GetDisplayUrl()}"
                };
                context.Result = new RedirectToActionResult("Login", "Account", url);
                return;
            }
            return;
        }
    }
}