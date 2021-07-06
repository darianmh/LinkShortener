using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Data.Link
{
    public class Link
    {
        [Key]
        public string ShortLink { get; set; }
        public string MainLink { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
