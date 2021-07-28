using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Data.User;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Data.Link
{
    public class Link
    {
        [Key]
        public string ShortLink { get; set; }
        public string MainLink { get; set; }
        public DateTime CreateDateTime { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public string IpV4 { get; set; }

        //np
        public virtual ApplicationUser User { get; set; }
    }
}
