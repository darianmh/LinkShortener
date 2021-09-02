namespace LinkShortener.Models.Link
{
    public class ShowAllLinksSortModel
    {
        public int AllPages { get; set; }
        public int CurrentPage { get; set; }
        public SortType SortType { get; set; }
        public SortBy SortBy { get; set; }

    }
}