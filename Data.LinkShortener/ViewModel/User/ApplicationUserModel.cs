using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.LinkShortener.Classes;
using Data.LinkShortener.Data.User;
using Microsoft.AspNetCore.Identity;

namespace Data.LinkShortener.ViewModel.User
{
    public class ApplicationUserModel
    {
        [Hidden]
        public int Id { get; set; }
        public virtual string UserName { get; set; }

        /// <summary>
        /// Gets or sets the normalized user name for this user.
        /// </summary>
        [Hidden]
        public virtual string NormalizedUserName { get; set; }

        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        [ProtectedPersonalData]
        [AdminShowItem(2)]
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the normalized email address for this user.
        /// </summary>
        [Hidden]
        public virtual string NormalizedEmail { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their email address.
        /// </summary>
        /// <value>True if the email address has been confirmed, otherwise false.</value>
        [PersonalData]
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a salted and hashed representation of the password for this user.
        /// </summary>
        [Hidden]
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// A random value that must change whenever a users credentials change (password changed, login removed)
        /// </summary>
        [Hidden]
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        /// A random value that must change whenever a user is persisted to the store
        /// </summary>
        [Hidden]
        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets a telephone number for the user.
        /// </summary>
        [ProtectedPersonalData]
        [AdminShowItem(3)]
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their telephone address.
        /// </summary>
        /// <value>True if the telephone number has been confirmed, otherwise false.</value>
        [PersonalData]
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if two factor authentication is enabled for this user.
        /// </summary>
        /// <value>True if 2fa is enabled, otherwise false.</value>
        [PersonalData]
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Gets or sets the date and time, in UTC, when any user lockout ends.
        /// </summary>
        /// <remarks>
        /// A value in the past means the user is not locked out.
        /// </remarks>
        public virtual DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if the user could be locked out.
        /// </summary>
        /// <value>True if the user could be locked out, otherwise false.</value>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the number of failed login attempts for the current user.
        /// </summary>
        [Hidden]
        public virtual int AccessFailedCount { get; set; }
        [AdminShowItem(1)]
        public string Name { get; set; }
        public string Family { get; set; }
        [Ignore]
        public string CurrentPassword { get; set; }
        [Ignore]
        public string NewPassword { get; set; }

        public bool IsSuperAdmin { get; set; }


        //extra entity
        [Display(Name = "نقش های کاربران")]
        [DbOptionList(typeof(ApplicationRole), true, true)]
        public List<int> SelectedRoles { get; set; }

        //np
        [Ignore]
        public virtual List<UserTokenModel> UserTokens { get; set; }
    }
}