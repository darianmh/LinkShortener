using System.Threading.Tasks;
using Data.LinkShortener.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Admin.LinkShortener.ViewComponents
{
    public class AdminPagination : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ListPaginationModel model)
        {
            return View(model);
        }

    }
}