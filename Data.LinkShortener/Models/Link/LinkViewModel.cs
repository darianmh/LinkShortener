using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.LinkShortener.Classes;
using Data.LinkShortener.Data.User;

namespace Data.LinkShortener.Models.Link
{
    public class LinkViewModel
    {
        [Key]
        [AdminShowItem(1)]
        [AdminKey]
        [Required]
        public string ShortLink { get; set; }
        [AdminShowItem(2)]
        [Required]
        public string MainLink { get; set; }
        public DateTime CreateDateTime { get; set; }
        [ForeignKey("User")]
        [DbOptionList(typeof(ApplicationUser), true)]
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
        [Ignore]
        public virtual ApplicationUser User { get; set; }
    }
}
