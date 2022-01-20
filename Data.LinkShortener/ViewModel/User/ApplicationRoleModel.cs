using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.LinkShortener.Classes;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Data.LinkShortener.ViewModel.User
{
    public class ApplicationRoleModel /*: IdentityRole<int>*/
    {
        /// <summary>
        /// Gets or sets the primary key for this role.
        /// </summary>
        [Hidden]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        [Display(Name = "نام")]
        [AdminShowItem(1)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the normalized name for this role.
        /// </summary>
        [Hidden]
        public virtual string NormalizedName { get; set; }

        /// <summary>
        /// A random value that should change whenever a role is persisted to the store
        /// </summary>
        [Hidden]
        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
        //np
        [Ignore]
        public virtual List<RoleAccessModel> RoleAccesses { get; set; }


        //extra entity
        [Ignore]
        [Display(Name = "دسترسی ها")]
        public List<SelectListItem> RoleAccessesList { get; set; }
        [Ignore]
        [Display(Name = "دسترسی ها")]
        public List<string> SelectedRoleAccesses { get; set; }
    }
}