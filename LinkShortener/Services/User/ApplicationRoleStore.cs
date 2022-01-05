using LinkShortener.Data;
using LinkShortener.Data.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LinkShortener.Services.User
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, ApplicationDbContext, int>,
        IRoleStore<ApplicationRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}