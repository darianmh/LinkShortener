using System.Threading.Tasks;
using LinkShortener.Services.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.ViewComponents
{
    public class EditCreateTemplate : ViewComponent
    {
        #region Fields

        #endregion
        #region Methods

        /// <summary>
        /// سازنده قالب برای صفحه ویرایش و ساخت
        /// </summary>
        /// <typeparam name="T">مدل موجود در صفحه</typeparam>
        /// <param name="model"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(object model, HttpContext httpContext)
        {
            var helper = new EditCreateTemplateHelper();
            var result = await helper.GetModel(model, httpContext);
            return View(result);
        }

        #endregion
        #region Ctor

        public EditCreateTemplate()
        {

        }
        #endregion

    }
}