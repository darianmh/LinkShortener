using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.LinkShortener.Data.Link;
using Data.LinkShortener.Models.Link;
using Services.LinkShortener.Services.LinkService;
using Services.LinkShortener.Services.Statics;

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
        public async Task<IActionResult> Index()
        {
            //find link item from context
            var linkCheck = HttpContext.Items.TryGetValue("LinkObject", out object? item);
            if (!linkCheck) return NotFound();
            //get CheckStatic object
            HttpContext.Items.TryGetValue("CheckStatic", out object? checkStatic);
            //get the right object
            var linkObject = await GetLinkObject(item);
            if (linkObject == null) return NotFound();
            //if (checkStatic != null && ((bool)checkStatic)) return RedirectToAction("Statics", new { shortLink = linkObject.ShortLink });
            if (checkStatic != null && ((bool)checkStatic)) return await Statics(shortLink: linkObject.ShortLink);
            return View("Index", linkObject);
        }

        public async Task<IActionResult> Statics(string shortLink)
        {
            var link = await _linkService.GetByShortLinkAsync(shortLink);
            if (shortLink == null) return NotFound();
            var statics = await _staticsService.GetStatics(link.ShortLink);
            var model = _staticsService.GetStaticsModel(statics, link);
            return View("Statics", model);
        }

        #endregion
        #region Utilities


        /// <summary>
        /// convert object to Link item safety
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private async Task<LinkRedirectModel> GetLinkObject(object item)
        {
            try
            {
                var linkObject = (Link)item;
                if (linkObject == null) return null;
                var linkModel = _linkService.ToLinkRedirectModel(linkObject);
                return linkModel;
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
