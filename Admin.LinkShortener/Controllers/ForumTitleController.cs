//using System.Threading.Tasks;
//using Data.LinkShortener.Classes;
//using Microsoft.AspNetCore.Mvc;

//namespace LinkShortener.Controllers.Admin
//{
//    [AdminFilterName(AdminControllerNames.Forum, "عنوان انجمن ها")]
//    public class ForumTitleController : BaseAdminController
//    {
//        #region Fields

//        private readonly IForumTitleService _service;

//        #endregion
//        #region Methods
//        public async Task<IActionResult> Index(int page = 1, int count = 10)
//        {
//            var all = await _service.GetAllInfoAsync(page, count);
//            //کسر یک عدد و سپس جمع آن برای رفع مشکل 10 تقسیم بر ده می باشد
//            var model = AdminModelHelper.GetIndexModel<ForumTitleModel, ForumTitle>(all, page, count);
//            return View(model);
//        }

//        public async Task<IActionResult> Details(int id)
//        {
//            var item = await _service.GetByIdAsync(id);
//            var model = item.ToModel();
//            return View(model);
//        }

//        public IActionResult Create()
//        {
//            return View("Edit", new ForumTitleModel());
//        }
//        public async Task<IActionResult> Edit(int id)
//        {
//            var item = await _service.GetByIdAsync(id);
//            var model = item.ToModel();
//            return View(model);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(ForumTitleModel model)
//        {
//            var userId = await GetUserId();
//            if (!ModelState.IsValid) return View("Edit", model);
//            model.OwnerId = userId;
//            var item = model.ToEntity();
//            await _service.InsertAsync(item);
//            return RedirectToAction("Details", new { id = item.Id });
//        }
//        [HttpPost]
//        public async Task<IActionResult> Edit(ForumTitleModel model)
//        {
//            if (!ModelState.IsValid) return View("Edit", model);
//            var item = model.ToEntity();
//            await _service.UpdateAsync(item);
//            return RedirectToAction("Details", new { id = model.Id });
//        }

//        public async Task<IActionResult> Delete(int id)
//        {
//            var item = await _service.DeleteAsync(id);
//            return RedirectToAction("Index");
//        }
//        #endregion
//        #region Utilities


//        #endregion
//        #region Ctor

//        public ForumTitleController(IForumTitleService service, IAdminModelHelper adminModelHelper) : base(adminModelHelper)
//        {
//            _service = service;
//        }
//        #endregion

//    }
//}
