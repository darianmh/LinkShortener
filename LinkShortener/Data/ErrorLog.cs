using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Models;

namespace LinkShortener.Data
{
    public class ErrorLog : BaseEntity
    {
        public string Text { get; set; }
        public string InnerText { get; set; }
    }
}
