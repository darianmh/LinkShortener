using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Data.LinkShortener.Classes.Mapper;
using Data.LinkShortener.Data;
using Data.LinkShortener.Data.Link;
using Data.LinkShortener.Models.IpLocation;
using Data.LinkShortener.Models.Statics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Services.LinkShortener.Services.ErrorLog;
using Services.LinkShortener.Services.Main;

namespace Services.LinkShortener.Services.Statics
{
    public class StaticsService : MainService<Data.LinkShortener.Data.Statics.Statics>, IStaticsService
    {
        #region Fields

        private readonly ApplicationDbContext _db;
        private readonly IErrorLogService _errorLogService;


        #endregion
        #region Methods

        /// <inheritdoc/>
        public async Task Insert(string shortLink, string ip, string refererUrl)
        {
            var model = new Data.LinkShortener.Data.Statics.Statics
            {
                CreateTimeTime = DateTime.Now,
                IpV4 = ip,
                ShortLink = shortLink,
                RefererUrl = refererUrl,
                CountryName = await GetCountryName(ip)
            };
            await _db.Statics.AddAsync(model);
            await _db.SaveChangesAsync();
        }



        /// <inheritdoc/>
        public async Task<List<Data.LinkShortener.Data.Statics.Statics>> GetStatics(string shortLink)
        {
            shortLink = shortLink.ToUpper();
            return await _db.Statics.Where(x => x.ShortLink.Equals(shortLink))
                .ToListAsync();
        }
        /// <summary>
        /// create statics report
        /// </summary>
        /// <param name="statics"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        public StaticModel GetStaticsModel(List<Data.LinkShortener.Data.Statics.Statics> statics, Link link)
        {

            StaticModel model = new StaticModel();
            model.Link = link.ToItemModel();
            try
            {
                model.StaticsByDates = GetStaticsByDates(statics);
                model.StaticsByMonths = GetStaticsByMonths(statics);
                model.TotalVisitCount = statics.Count;
                model.StaticsByCountries = GetStaticsByCountries(statics);
                model.StaticsByDomains = GetStaticsByDomains(statics);
            }
            catch (Exception e)
            {
                _errorLogService.Insert(e.Message, e.InnerException?.Message + "\r\n" + e.StackTrace);
                //ignore
            }
            return model;
        }


        /// <inheritdoc/>
        public async Task<string> GetCountryName(string ip)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://ip-geolocation-ipwhois-io.p.rapidapi.com/json/?ip=" + ip),
                    Headers =
                    {
                        { "x-rapidapi-host", "ip-geolocation-ipwhois-io.p.rapidapi.com" },
                        { "x-rapidapi-key", "d8f626c39amshfb377c17d38608ep103466jsnc9be2a35aed2" },
                    },
                };
                LocationRequestResponse model = null;
                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<LocationRequestResponse>(body);
                return model?.country;
            }
            catch (Exception e)
            {
                return "";
            }
        }


        #endregion
        #region Utilities


        private List<StaticsByMonth> GetStaticsByMonths(List<Data.LinkShortener.Data.Statics.Statics> statics)
        {
            var val = statics.GroupBy(x => x.CreateTimeTime.ToString("yyyy MMMMM")).Select(x => new StaticsByMonth
            {
                Month = x.Key,
                Date = x.First().CreateTimeTime.Date,
                Count = x.Count()
            }).OrderBy(x => x.Date).Reverse().ToList();
            return val;
        }

        /// <summary>
        /// create domain statics
        /// which domain and which pages are link to our page
        /// </summary>
        /// <param name="statics"></param>
        /// <returns></returns>
        private List<StaticsByDomain> GetStaticsByDomains(List<Data.LinkShortener.Data.Statics.Statics> statics)
        {

            var result = statics.Where(x => !string.IsNullOrEmpty(x.RefererUrl)).GroupBy(x => (new Uri(global::Services.LinkShortener.Services.LinkService.LinkService.Correct(x.RefererUrl))).Host).Select(x => new StaticsByDomain
            {
                Domain = x.Key,
                Count = x.Count(),
                Urls = x.GroupBy(z => z.RefererUrl).Select(c => new StaticsByUrl()
                {
                    Count = c.Count(),
                    Url = c.Key
                }).ToList()
            }).ToList();

            return result;
        }

        /// <summary>
        /// group visitors by country
        /// </summary>
        /// <param name="statics"></param>
        /// <returns></returns>
        private List<StaticsByCountry> GetStaticsByCountries(List<Data.LinkShortener.Data.Statics.Statics> statics)
        {
            var val = statics.GroupBy(x => x.CountryName).Select(x => new StaticsByCountry
            {
                CountryName = x.Key,
                Count = x.Count()
            }).ToList();
            return val;
        }

        /// <summary>
        /// group visitors by visit date
        /// </summary>
        /// <param name="statics"></param>
        /// <returns></returns>
        private List<StaticsByDate> GetStaticsByDates(List<Data.LinkShortener.Data.Statics.Statics> statics)
        {
            var val = statics.GroupBy(x => x.CreateTimeTime.Date).Select(x => new StaticsByDate
            {
                Date = x.Key.Date,
                Count = x.Count()
            }).OrderBy(x => x.Date).Reverse().ToList();
            return val;
        }

        #endregion
        #region Ctor
        public StaticsService(ApplicationDbContext db, IErrorLogService errorLogService) : base(db)
        {
            _db = db;
            _errorLogService = errorLogService;
        }

        #endregion


    }
}