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
    [AdminFilterName(AdminControllerNames.User, "نقش های تعریف شده")]
    public class ApplicationRoleController : BaseAdminController
    {
        #region Fields

        private readonly IApplicationRoleService _service;
        private readonly ApplicationRoleManager _applicationRoleManager;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly IRoleAccessService _roleAccessService;

        #endregion
        #region Methods
        public async Task<IActionResult> Index(int page = 1, int count = 10)
        {
            var all = await _service.GetAllInfoAsync(page, count);
            //کسر یک عدد و سپس جمع آن برای رفع مشکل 10 تقسیم بر ده می باشد
            var model = AdminModelHelper.GetIndexModel<ApplicationRoleModel, ApplicationRole>(all, page, count);
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            var model = item.ToModel();
            model.RoleAccessesList = await _roleAccessService.GetRoleAccesses(roleId: model.Id);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = new ApplicationRoleModel();
            model.RoleAccessesList = await _roleAccessService.GetRoleAccesses(roleId: 0);
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            var model = item.ToModel();
            model.RoleAccessesList = await _roleAccessService.GetRoleAccesses(roleId: id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                model.RoleAccessesList = await _roleAccessService.GetRoleAccesses(roleId: model.Id);
                return View(model);
            }
            var item = model.ToEntity();
            item.Name = _applicationUserManager.NormalizeName(item.Name);
            item.NormalizedName = _applicationUserManager.NormalizeName(item.NormalizedName);
            await _service.InsertAsync(item);
            await _roleAccessService.SetRoleAccesses(item.Id, model.SelectedRoleAccesses);
            return RedirectToAction("Details", new { id = item.Id });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                model.RoleAccessesList = await _roleAccessService.GetRoleAccesses(roleId: model.Id);
                return View(model);
            }
            var item = model.ToEntity();
            item.Name = _applicationUserManager.NormalizeName(item.Name);
            item.NormalizedName = _applicationUserManager.NormalizeName(item.NormalizedName);
            await _service.UpdateAsync(item);
            await _roleAccessService.SetRoleAccesses(item.Id, model.SelectedRoleAccesses);
            return RedirectToAction("Details", new { id = model.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _roleAccessService.DeleteAllByRoleIdASync(id);
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        #endregion
        #region Utilities


        #endregion
        #region Ctor

        public ApplicationRoleController(IApplicationRoleService service, ApplicationRoleManager applicationRoleManager, ApplicationUserManager applicationUserManager, IRoleAccessService roleAccessService, IAdminModelHelper adminModelHelper) : base(adminModelHelper)
        {
            _service = service;
            _applicationRoleManager = applicationRoleManager;
            _applicationUserManager = applicationUserManager;
            _roleAccessService = roleAccessService;
        }
        #endregion


    }
}
