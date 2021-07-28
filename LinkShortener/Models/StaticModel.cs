using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Data.Link;
using LinkShortener.Data.Statics;

namespace LinkShortener.Models
{
    public class StaticModel
    {
        public Data.Link.Link Link { get; set; }
        public List<Statics> Statics { get; set; }
    }
}
