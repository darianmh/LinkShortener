using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinkShortener.Classes;

namespace LinkShortener.ViewModel.User
{
    /// <summary>
    /// دسترسی های نقش
    /// </summary>
    public class RoleAccessModel : BaseEntityModel
    {
        /// <summary>
        /// هر اکشنی یک اتریبیوت دارد 
        ///که نام و وظیفه آنرا تعیین می کند
        /// </summary>
        [Display(Name = "نام اتریبیوت")]
        [Required(ErrorMessage = "{0} الزامی است")]
        [AdminShowItem(1)]
        public string AttrName { get; set; }

        /// <summary>
        /// نقش
        /// </summary>
        [ForeignKey("ApplicationRole")]
        [Display(Name = "نقش")]
        [Required(ErrorMessage = "{0} الزامی است")]
        [AdminShowItem(2)]
        public int RoleId { get; set; }


        //np
        public virtual ApplicationRoleModel ApplicationRole { get; set; }
    }
}