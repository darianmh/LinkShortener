using Services.LinkShortener.Services.Helper;

namespace Services.LinkShortener.Classes
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