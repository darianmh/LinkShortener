using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Data.Link;
using LinkShortener.Models;
using LinkShortener.Services.LinkService;
using LinkShortener.Services.Statics;

namespace LinkShortener.Controllers
{
    public class RoutController : Controller
    {
        #region Fields

        private readonly IStaticsService _staticsService;
        private readonly ILinkService _linkService;


        #endregion
        #region Methods

        /// <summary>
        /// when short link found in db, this method will redirect user to the link
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //find link item from context
            var linkCheck = HttpContext.Items.TryGetValue("LinkObject", out object? item);
            if (!linkCheck) return NotFound();
            //get CheckStatic object
            HttpContext.Items.TryGetValue("CheckStatic", out object? CheckStatic);
            //get the right object
            var linkObject = GetLinkObject(item);
            if (linkObject == null) return NotFound();
            if (CheckStatic != null && ((bool)CheckStatic)) return RedirectToAction("Statics", new { shortLink = linkObject.ShortLink });
            return View(linkObject);
        }

        public async Task<IActionResult> Statics(string shortLink)
        {
            var link = await _linkService.GetByShortLinkAsync(shortLink);
            if (shortLink == null) return NotFound();
            var statics = await _staticsService.GetStatics(link.ShortLink);
            var model = new StaticModel
            {
                Link = link,
                Statics = statics
            };
            return View(model);
        }

        #endregion
        #region Utilities


        /// <summary>
        /// convert object to Link item safety
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Link GetLinkObject(object item)
        {
            try
            {
                var linkObject = (Link)item;
                return linkObject;
            }
            catch
            {
                //ignore
            }

            return null;
        }

        #endregion
        #region Ctor
        public RoutController(IStaticsService staticsService, ILinkService linkService)
        {
            _staticsService = staticsService;
            _linkService = linkService;
        }

        #endregion

    }
}
