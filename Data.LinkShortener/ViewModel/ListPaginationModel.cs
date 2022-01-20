namespace Data.LinkShortener.ViewModel
{
    public class ListPaginationModel
    {
        public bool HasNext { get; set; }
        public bool HasPre { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
        public int PagesCount { get; set; }

        public ListPaginationModel(bool hasNext, bool hasPre, int page, int count, int pagesCount)
        {
            HasNext = hasNext;
            HasPre = hasPre;
            Page = page;
            Count = count;
            PagesCount = pagesCount;
        }
    }
}