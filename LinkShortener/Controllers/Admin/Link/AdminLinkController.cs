using System.Threading.Tasks;
using LinkShortener.Classes;
using LinkShortener.Classes.Mapper;
using LinkShortener.Models.Link;
using LinkShortener.Services.Helper;
using LinkShortener.Services.LinkService;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers.Admin.Link
{
    [AdminFilterName(AdminControllerNames.Link, "لینک ها")]
    public class AdminLinkController : BaseAdminController
    {
        #region Fields

        private readonly ILinkService _service;

        #endregion
        #region Methods
        public async Task<IActionResult> Index(int page = 1, int count = 10)
        {
            var all = await _service.GetAllInfoAsync(page, count);
            //کسر یک عدد و سپس جمع آن برای رفع مشکل 10 تقسیم بر ده می باشد
            var model = AdminModelHelper.GetIndexModel<LinkViewModel, Data.Link.Link>(all, page, count);
            return View(model);
        }

        public async Task<IActionResult> Details(string ShortLink)
        {
            var item = await _service.GetByShortLinkAsync(ShortLink);
            var model = item.ToModel();
            return View(model);
        }

        public IActionResult Create()
        {
            return View("Edit", new LinkViewModel());
        }
        public async Task<IActionResult> Edit(string ShortLink)
        {
            var item = await _service.GetByShortLinkAsync(ShortLink);
            var model = item.ToModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LinkViewModel model)
        {
            var userId = await GetUserId();
            if (!ModelState.IsValid) return View("Edit", model);
            model.UserId = userId;
            var item = model.ToEntity();
            await _service.InsertAsync(item);
            return RedirectToAction("Details", new { ShortLink = item.ShortLink });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(LinkViewModel model)
        {
            if (!ModelState.IsValid) return View("Edit", model);
            var item = model.ToEntity();
            await _service.UpdateAsync(item);
            return RedirectToAction("Details", new { ShortLink = model.ShortLink });
        }

        public async Task<IActionResult> Delete(string ShortLink)
        {
            var item = await _service.GetByShortLinkAsync(ShortLink);
            if (item != null) await _service.DeleteAsync(item);
            return RedirectToAction("Index");
        }
        #endregion
        #region Utilities


        #endregion
        #region Ctor

        public AdminLinkController(ILinkService service, IAdminModelHelper adminModelHelper) : base(adminModelHelper)
        {
            _service = service;
        }
        #endregion

    }
}
