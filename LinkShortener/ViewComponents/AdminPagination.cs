using System.Threading.Tasks;
using LinkShortener.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.ViewComponents
{
    public class AdminPagination : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ListPaginationModel model)
        {
            return View(model);
        }

    }
}