using System.Collections.Generic;
using System.Threading.Tasks;
using LinkShortener.Data.Link;
using LinkShortener.Services.LinkService;
using LinkShortener.Services.Statics;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers.Admin
{
    public class AllLinksController : Controller
    {
        #region Fields

        private readonly ILinkService _linkService;
        private readonly IStaticsService _staticsService;


        #endregion
        #region Methods

        public async Task<IActionResult> Index()
        {
            var allLinks = await _linkService.GetAllLinksAsync();
            if (allLinks == null) allLinks = new List<Link>();
            var linkModel = await _linkService.ToAdminLinkModel(allLinks, _staticsService);
            return View(linkModel);
        }
        #endregion
        #region Utilities


        #endregion
        #region Ctor


        public AllLinksController(ILinkService linkService, IStaticsService staticsService)
        {
            _linkService = linkService;
            _staticsService = staticsService;
        }
        #endregion


    }
}
