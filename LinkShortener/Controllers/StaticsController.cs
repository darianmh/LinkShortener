using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Models;
using LinkShortener.Models.Api;
using LinkShortener.Models.Statics;
using LinkShortener.Services.LinkService;
using LinkShortener.Services.Statics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticsController : ControllerBase
    {
        #region Fields

        private readonly IStaticsService _staticsService;
        private readonly ILinkService _linkService;

        #endregion
        #region Methods

        [HttpGet("{shortLink?}")]
        public async Task<ApiResponse<StaticModel>> Get(string shortLink)
        {
            var link = await _linkService.GetByShortLinkAsync(shortLink);
            if (shortLink == null) return new ApiResponse<StaticModel>()
            {
                Ok = false,
                Error = "لینک مورد نظر یافت نشد."
            };
            var statics = await _staticsService.GetStatics(link.ShortLink);
            var model = _staticsService.GetStaticsModel(statics, link);
            return new ApiResponse<StaticModel>()
            {
                Ok = true,
                Data = model
            };
        }

        #endregion
        #region Utilities


        #endregion
        #region Ctor

        public StaticsController(IStaticsService staticsService, ILinkService linkService)
        {
            _staticsService = staticsService;
            _linkService = linkService;
        }

        #endregion

    }
}
