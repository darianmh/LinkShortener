using System.Collections.Generic;

namespace Data.LinkShortener.Models.Link
{
    public class ShowAllLinksModel<TLinks>
    {
        public List<TLinks> AllLinks { get; set; }
        public ShowAllLinksSortModel SortModel { get; set; }
    }
}