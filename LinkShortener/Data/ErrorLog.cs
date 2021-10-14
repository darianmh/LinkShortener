using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Data
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string InnerText { get; set; }
        public DateTime DateTime { get; set; }
    }
}
