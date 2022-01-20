using Admin.LinkShortener.Classes;
using Data.LinkShortener.Classes;
using Microsoft.AspNetCore.Mvc;
using Services.LinkShortener.Classes;
using Services.LinkShortener.Services.Helper;

namespace Admin.LinkShortener.Controllers
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
