using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models.Link
{
    public class AdminLinkModel
    {
        public string ShortLink { get; set; }
        public string MainLink { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int VisitCount { get; set; }
    }
}
