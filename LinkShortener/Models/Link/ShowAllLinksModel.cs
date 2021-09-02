using System.Collections.Generic;

namespace LinkShortener.Models.Link
{
    public class ShowAllLinksModel<TLinks>
    {
        public List<TLinks> AllLinks { get; set; }
        public ShowAllLinksSortModel SortModel { get; set; }
    }
}