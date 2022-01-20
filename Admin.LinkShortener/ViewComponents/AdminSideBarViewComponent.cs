using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Services.LinkShortener.Services;
using Services.LinkShortener.Services.User;

namespace Admin.LinkShortener.ViewComponents
{
    public class AdminSideBarViewComponent : ViewComponent
    {
        private readonly ApplicationUserManager _applicationUserManager;

        public AdminSideBarViewComponent(ApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var adminLinks = await AdminPanelService.GetLinks(_applicationUserManager, User.Identity?.Name, Request.GetEncodedPathAndQuery());
            return View(adminLinks);
        }

    }
}