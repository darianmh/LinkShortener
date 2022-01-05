using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinkShortener.Models;
using Newtonsoft.Json;

namespace LinkShortener.Data.User
{
    /// <summary>
    /// دسترسی های نقش
    /// </summary>
    public class RoleAccess : BaseEntity
    {
        /// <summary>
        /// هر اکشنی یک اتریبیوت دارد 
        ///که نام و وظیفه آنرا تعیین می کند
        /// </summary>
        [Display(Name = "نام اتریبیوت")]
        [Required(ErrorMessage = "{0} الزامی است")]
        public string AttrName { get; set; }

        /// <summary>
        /// نقش
        /// </summary>
        [ForeignKey("ApplicationRole")]
        [Display(Name = "نقش")]
        [Required(ErrorMessage = "{0} الزامی است")]
        public int RoleId { get; set; }


        //np
        [JsonIgnore]
        public virtual ApplicationRole ApplicationRole { get; set; }
    }
}