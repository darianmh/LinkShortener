using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Data.LinkShortener.Data.User;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Data.LinkShortener.Data.Link
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
        /// <summary>
        /// is public to show in global link viewer
        /// </summary>
        public bool IsPublic { get; set; }
        /// <summary>
        /// showing headers info like meta tags and keywords
        /// </summary>
        public string HeaderText { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// auto generated from header meta title
        /// </summary>
        public string LinkTitle { get; set; }
        /// <summary>
        /// holds total visit count => in future we should replace db with nosql to improve performance 
        /// </summary>
        public int TotalVisitCount { get; set; }
        //np
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }
    }
}
