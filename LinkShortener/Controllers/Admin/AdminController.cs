using LinkShortener.Classes;
using LinkShortener.Services.Helper;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers.Admin
{
    [AdminFilterName(AdminControllerNames.Dashboard, "داشبرد")]
    public class AdminController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }

        public AdminController(IAdminModelHelper adminModelHelper) : base(adminModelHelper)
        {
        }
    }
}
