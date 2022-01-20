using System.Collections.Generic;
using System.Threading.Tasks;
using Data.LinkShortener.Data.Link;
using Data.LinkShortener.Models.Link;
using Microsoft.AspNetCore.Mvc;
using Services.LinkShortener.Services.LinkService;
using Services.LinkShortener.Services.Statics;

namespace LinkShortener.Controllers.Admin
{
    public class AllLinksController : Controller
    {
        #region Fields

        private readonly ILinkService _linkService;
        private readonly IStaticsService _staticsService;


        #endregion
        #region Methods

        public IActionResult Index(SortType sortType = SortType.Descending, SortBy sortBy = SortBy.Date, int pageNumber = 1)
        {
            var links = _linkService.GetAllLinksShowModelAsync(sortBy, sortType, pageNumber);
            return View(links);
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
