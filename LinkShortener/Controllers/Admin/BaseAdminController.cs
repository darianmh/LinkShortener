using LinkShortener.Services.Helper;

namespace LinkShortener.Controllers.Admin
{
    public class BaseAdminController : MainBaseController
    {
        protected readonly IAdminModelHelper AdminModelHelper;

        public BaseAdminController(IAdminModelHelper adminModelHelper)
        {
            AdminModelHelper = adminModelHelper;
        }

    }
}