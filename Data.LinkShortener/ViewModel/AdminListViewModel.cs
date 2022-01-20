using System.Collections.Generic;

namespace Data.LinkShortener.ViewModel
{
    public class AdminListViewModel<T>
    {
        public AdminListViewModel(bool hasNext, bool hasPre, List<T> items, int page, int count, int pagesCount)
        {
            AdminListPaginationModel = new ListPaginationModel(hasNext, hasPre, page, count, pagesCount);
            Items = items;
        }

        public AdminListViewModel()
        {
            Items = new List<T>();
        }

        public List<T> Items { get; set; }
        public ListPaginationModel AdminListPaginationModel { get; set; }

    }
}
