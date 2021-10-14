using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models.Link
{
    public class LinkItemModel
    {
        public string ShortLink { get; set; }
        public string MainLink { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Description { get; set; }
        public string LinkTitle { get; set; }
        public int TotalVisitCount { get; set; }
    }
}
