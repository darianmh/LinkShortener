using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Data.Link;
using LinkShortener.Data.Statics;
using LinkShortener.Models.Link;

namespace LinkShortener.Models
{
    public class StaticModel
    {
        public LinkItemModel Link { get; set; }
        public List<Statics> Statics { get; set; }
    }
}
