﻿using System.Collections.Generic;
using System.Threading.Tasks;
using LinkShortener.Data.Link;
using LinkShortener.Models.Link;
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
