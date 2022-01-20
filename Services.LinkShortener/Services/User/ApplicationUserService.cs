using Data.LinkShortener.Data;
using Data.LinkShortener.Data.User;
using Services.LinkShortener.Services.Main;

namespace Services.LinkShortener.Services.User
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