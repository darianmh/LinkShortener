using System.Threading.Tasks;
using LinkShortener.Classes;
using LinkShortener.Classes.Mapper;
using LinkShortener.Data.User;
using LinkShortener.Services.Helper;
using LinkShortener.Services.User;
using LinkShortener.ViewModel.User;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers.Admin.User
{
    [AdminFilterName(AdminControllerNames.User, "دسترسی نقش ها")]
    public class RoleAccessController : BaseAdminController
    {
        #region Fields

        private readonly IRoleAccessService _service;

        #endregion
        #region Methods
        public async Task<IActionResult> Index(int page = 1, int count = 10)
        {
            var all = await _service.GetAllInfoAsync(page, count);
            //کسر یک عدد و سپس جمع آن برای رفع مشکل 10 تقسیم بر ده می باشد
            var model = AdminModelHelper.GetIndexModel<RoleAccessModel, RoleAccess>(all, page, count);
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            var model = item.ToModel();
            return View(model);
        }

        public IActionResult Create()
        {
            return View(new RoleAccessModel());
        }
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            var model = item.ToModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleAccessModel model)
        {
            var item = model.ToEntity();
            await _service.InsertAsync(item);
            return RedirectToAction("Details", new { id = item.Id });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleAccessModel model)
        {
            var item = model.ToEntity();
            await _service.UpdateAsync(item);
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

        public RoleAccessController(IRoleAccessService service, IAdminModelHelper adminModelHelper) : base(adminModelHelper)
        {
            _service = service;
        }
        #endregion


    }
}
