using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Data.LinkShortener.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.LinkShortener.Data.Statics
{
    public class Statics : BaseEntity
    {
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
