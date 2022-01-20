using System.Collections.Generic;

namespace Data.LinkShortener.ViewModel
{
    public class AdminShowListViewModel
    {
        public ListPaginationModel AdminListPaginationModel { get; set; }
        /// <summary>
        /// کلید اول آیدی و کلید دوم سایر مقدار ها است
        /// </summary>
        public List<AdminShowListKeyValues> Values { get; set; }
        public List<string> Keys { get; set; }
        public string Controller { get; set; }
    }
}