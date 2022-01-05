using LinkShortener.Classes;
using LinkShortener.ViewModel;

namespace LinkShortener.Services.Helper
{
    public interface IAdminModelHelper
    {
        /// <summary>
        /// ایجاد مدل برای صفحه ایندکس پنل ادمین
        /// </summary>
        /// <returns></returns>
        AdminListViewModel<TModel> GetIndexModel<TModel, TBase>(DbModelInfo<TBase> all, int page, int count);
    }
}
