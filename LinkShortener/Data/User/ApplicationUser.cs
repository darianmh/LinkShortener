using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkShortener.Data.User
{
    public class ApplicationUser : IdentityUser<int>
    {

        //np
        public virtual IList<Link.Link> Links { get; set; }
    }
}
