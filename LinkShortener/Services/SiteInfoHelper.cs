using System.Threading.Tasks;
using LinkShortener.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkShortener.Services
{
    public static class SiteInfoHelper
    {
        #region Fields


        #endregion
        #region Methods

        public static async Task<string> GetUserName(this IHtmlHelper helper)
        {
            var user = helper.ViewContext.HttpContext.User.Identity?.Name;
            if (user == null) return "";
            var userManager =
                (ApplicationUserManager)helper.ViewContext.HttpContext.RequestServices.GetService(
                    typeof(ApplicationUserManager));
            if (userManager == null) return "";
            return await userManager.GetUSerDisplayNameAsync(user);
        }
        public static async Task<string> GetSiteLogo(this IHtmlHelper helper)
        {
            return "../../icon.png";
        }
        public static async Task<string> GetSiteName(this IHtmlHelper helper)
        {
            return "داریان - لینک دات آی آر";
        }
        #endregion
        #region Utilities


        #endregion

    }
}
