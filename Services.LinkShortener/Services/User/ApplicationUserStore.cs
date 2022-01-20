using System;
using System.Linq;
using System.Threading.Tasks;
using Data.LinkShortener.Data;
using Data.LinkShortener.Data.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Services.LinkShortener.Services.User
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, int>,
        IUserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
        /// <summary>
        /// find user from phone or mobile or username
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> FindAsync(string name)
        {
            name = name.Trim();
            int.TryParse(name, out var intName);
            return Context.Users.AsEnumerable().FirstOrDefault(x =>
                (x.Id == intName) ||
                (x.UserName != null && x.UserName.Equals(name, StringComparison.OrdinalIgnoreCase)) ||
                (x.Email != null && x.Email.Equals(name, StringComparison.OrdinalIgnoreCase)) ||
                (x.PhoneNumber != null && x.PhoneNumber.Equals(name, StringComparison.OrdinalIgnoreCase)))??new ApplicationUser();
        }
    }
}