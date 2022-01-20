using Data.LinkShortener.Data;
using Data.LinkShortener.Data.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Services.LinkShortener.Services.User
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, ApplicationDbContext, int>,
        IRoleStore<ApplicationRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}