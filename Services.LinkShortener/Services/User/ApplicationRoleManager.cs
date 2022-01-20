using System.Collections.Generic;
using Data.LinkShortener.Data.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Services.LinkShortener.Services.User
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(ApplicationRoleStore store, IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<ApplicationRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}