using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using LinkShortener.Data.Link;
using LinkShortener.Models.Api;
using LinkShortener.Services.Helper;
using LinkShortener.Services.LinkService;

namespace LinkShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        #region Fields

        private readonly ILinkService _linkService;


        #endregion
        #region Methods

        /// <summary>
        /// create new link
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        [HttpGet("{link?}")]
        public async Task<ApiResponse<Link>> Get(string link = "")
        {
            //check value
            if (string.IsNullOrEmpty(link)) return new ApiResponse<Link>()
            {
                Ok = false,
                Error = "Link is not in correct format"
            };
            link = FormatLink(link);
            //check link
            var checkLink = Helper.CheckLink(link);
            if (!checkLink) return new ApiResponse<Link>()
            {
                Ok = false,
                Error = "Link is not in correct format"
            };
            //create link
            var linkModel = await _linkService.Create(link);
            //if null => something is wrong and link did not create
            if (linkModel == null) return new ApiResponse<Link>()
            {
                Ok = false,
                Error = "Can not create your link right now. Please try later"
            };
            //link is ready
            return new ApiResponse<Link>()
            {
                Ok = true,
                Data = linkModel
            };
        }


        #endregion
        #region Utilities

        /// <summary>
        /// link is convert from serialized to deserialized format
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        private string FormatLink(string link)
        {
            return HttpUtility.UrlDecode(link);
        }

        #endregion
        #region Ctor

        public LinksController(ILinkService linkService)
        {
            _linkService = linkService;
        }
        #endregion


    }
}
