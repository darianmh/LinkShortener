using Data.LinkShortener.Data.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.LinkShortener.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public virtual DbSet<Link.Link> Links { get; set; }
        public virtual DbSet<Statics.Statics> Statics { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
    }
}
