using System.Collections.Generic;
using LinkShortener.Models.Link;

namespace LinkShortener.Models.Statics
{
    public class StaticModel
    {
        public LinkItemModel Link { get; set; }
        public int TotalVisitCount { get; set; }
        public List<StaticsByCountry> StaticsByCountries { get; set; }
        public List<StaticsByDate> StaticsByDates { get; set; }
        public List<StaticsByMonth> StaticsByMonths { get; set; }
        public List<StaticsByDomain> StaticsByDomains { get; set; }

    }
}
