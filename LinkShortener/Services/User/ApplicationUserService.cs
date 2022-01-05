using LinkShortener.Data;
using LinkShortener.Data.User;
using LinkShortener.Services.Main;

namespace LinkShortener.Services.User
{
    public class ApplicationUserService : MainServiceNonBaseEntity<ApplicationUser>, IApplicationUserService
    {
        #region Fields


        #endregion
        #region Methods


        #endregion
        #region Utilities


        #endregion
        #region Ctor
        public ApplicationUserService(ApplicationDbContext db) : base(db)
        {
        }
        #endregion
    }
}