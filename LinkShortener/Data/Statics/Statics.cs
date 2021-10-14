using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Data.Statics
{
    public class Statics
    {
        [Key]
        public int Id { get; set; }
        public string ShortLink { get; set; }
        public string IpV4 { get; set; }
        public DateTime CreateTimeTime { get; set; }
        /// <summary>
        /// link is clicked in which site
        /// </summary>
        public string RefererUrl { get; set; }
        public string CountryName { get; set; }
    }
}
