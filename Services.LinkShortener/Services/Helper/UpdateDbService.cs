using System;
using System.Linq;
using System.Threading.Tasks;
using Services.LinkShortener.Services.ErrorLog;
using Services.LinkShortener.Services.LinkService;
using Services.LinkShortener.Services.Statics;

namespace Services.LinkShortener.Services.Helper
{
    public class UpdateDbService
    {
        #region Fields

        private readonly IStaticsService _staticsService;
        private readonly ILinkService _linkService;
        private readonly IErrorLogService _errorLogService;


        #endregion
        #region Methods
        public async Task UpdateIpLocations()
        {
            var allStatics = await _staticsService.GetAllAsync();
            allStatics = allStatics
                .Where(x => string.IsNullOrEmpty(x.CountryName) || string.IsNullOrEmpty(x.RefererUrl)).ToList();
            foreach (var statics in allStatics)
            {
                if (!string.IsNullOrEmpty(statics.IpV4))
                {
                    if (string.IsNullOrEmpty(statics.CountryName))
                    {
                        var countryName = await _staticsService.GetCountryName(statics.IpV4);
                        statics.CountryName = countryName;
                    }
                };

                if (string.IsNullOrEmpty(statics.RefererUrl))
                    statics.RefererUrl = "https://darianteam.ir";
                _staticsService.Update(statics);
            }
        }

        public async Task UpdateTitleAndDescription()
        {
            var allLinks = await _linkService.GetAllLinksAsync();
            allLinks = allLinks.Where(x => string.IsNullOrEmpty(x.Description) || string.IsNullOrEmpty(x.LinkTitle)).ToList();
            foreach (var link in allLinks)
            {
                try
                {
                    var temp = await _linkService.GetLinkInfo(link, link.MainLink);
                    link.Description = temp.Description;
                    link.HeaderText = temp.HeaderText;
                    link.LinkTitle = temp.LinkTitle;
                    _linkService.Update(link);
                }
                catch (Exception e)
                {
                    _errorLogService.Insert(e.Message + "/// " + link.MainLink, e.InnerException?.Message + "\r\n" + e.StackTrace);
                }
            }
        }

        #endregion
        #region Utilities


        #endregion
        #region Ctor

        public UpdateDbService(IStaticsService staticsService, ILinkService linkService, IErrorLogService errorLogService)
        {
            _staticsService = staticsService;
            _linkService = linkService;
            _errorLogService = errorLogService;
        }
        #endregion


    }
}
