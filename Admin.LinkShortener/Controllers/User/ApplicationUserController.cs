using System.Threading.Tasks;
using Admin.LinkShortener.Classes;
using Data.LinkShortener.Classes;
using Data.LinkShortener.Classes.Mapper;
using Data.LinkShortener.Data.User;
using Data.LinkShortener.ViewModel.User;
using Microsoft.AspNetCore.Mvc;
using Services.LinkShortener.Classes;
using Services.LinkShortener.Services.Helper;
using Services.LinkShortener.Services.User;

namespace Admin.LinkShortener.Controllers.User
{
    [AdminFilterName(AdminControllerNames.User, "کاربران")]
    public class ApplicationUserController : BaseAdminController
    {
        #region Fields

        private readonly IApplicationUserService _service;
        private readonly ApplicationUserManager _applicationUserManager;
        #endregion
        #region Methods
        public async Task<IActionResult> Index(int page = 1, int count = 10)
        {
            var all = await _service.GetAllInfoAsync(page, count);
            //کسر یک عدد و سپس جمع آن برای رفع مشکل 10 تقسیم بر ده می باشد
            var model = AdminModelHelper.GetIndexModel<ApplicationUserModel, ApplicationUser>(all, page, count);
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            var model = item.ToModel();
            model.SelectedRoles = await _applicationUserManager.GetSelectedRolesByUser(id);
            return View(model);
        }

        public IActionResult Create()
        {
            return View(new ApplicationUserModel());
        }
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            var model = item.ToModel();
            model.SelectedRoles = await _applicationUserManager.GetSelectedRolesByUser(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUserModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var item = model.ToEntity();
            await _service.InsertAsync(item);
            await _applicationUserManager.SetUserRoles(item, model.SelectedRoles);
            return RedirectToAction("Details", new { id = item.Id });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUserModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var item = model.ToEntity();
            await _service.UpdateAsync(item);
            await _applicationUserManager.SetUserRoles(item, model.SelectedRoles);
            return RedirectToAction("Details", new { id = model.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        #endregion
        #region Utilities


        #endregion
        #region Ctor

        public ApplicationUserController(IApplicationUserService service, ApplicationUserManager applicationUserManager, IAdminModelHelper adminModelHelper) : base(adminModelHelper)
        {
            _service = service;
            _applicationUserManager = applicationUserManager;
        }
        #endregion


    }
}
