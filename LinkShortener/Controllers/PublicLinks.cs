using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.LinkShortener.Models.Link;
using Services.LinkShortener.Services.LinkService;

namespace LinkShortener.Controllers
{
    public class PublicLinks : Controller
    {
        #region Fields

        private readonly ILinkService _linkService;


        #endregion
        #region Methods

        public IActionResult Index(SortType sortType = SortType.Descending, SortBy sortBy = SortBy.Date, int pageNumber = 1)
        {
            var links = _linkService.GetGlobalLinksShowModelAsync(sortBy, sortType, pageNumber);
            return View(links);
        }
        #endregion
        #region Utilities


        #endregion
        #region Ctor


        public PublicLinks(ILinkService linkService)
        {
            _linkService = linkService;
        }
        #endregion

    }
}
