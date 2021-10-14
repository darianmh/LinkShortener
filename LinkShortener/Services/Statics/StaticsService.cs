using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LinkShortener.Classes.Mapper;
using LinkShortener.Data;
using LinkShortener.Data.Link;
using LinkShortener.Models.IpLocation;
using LinkShortener.Models.Statics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LinkShortener.Services.Statics
{
    public class StaticsService : IStaticsService
    {
        #region Fields

        private readonly ApplicationDbContext _db;


        #endregion
        #region Methods

        /// <inheritdoc/>
        public async Task Insert(string shortLink, string ip, string refererUrl)
        {
            var model = new Data.Statics.Statics
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
        public async Task<List<Data.Statics.Statics>> GetStatics(string shortLink)
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
        public StaticModel GetStaticsModel(List<Data.Statics.Statics> statics, Link link)
        {

            var model = new StaticModel
            {
                Link = link.ToItemModel(),
                StaticsByDates = GetStaticsByDates(statics),
                StaticsByMonths = GetStaticsByMonths(statics),
                TotalVisitCount = statics.Count,
                StaticsByCountries = GetStaticsByCountries(statics),
                StaticsByDomains = GetStaticsByDomains(statics)
            };
            return model;
        }

        private List<StaticsByMonth> GetStaticsByMonths(List<Data.Statics.Statics> statics)
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
        private List<StaticsByDomain> GetStaticsByDomains(List<Data.Statics.Statics> statics)
        {
            var result = statics.GroupBy(x => (new Uri(x.RefererUrl)).Host).Select(x => new StaticsByDomain
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
        private List<StaticsByCountry> GetStaticsByCountries(List<Data.Statics.Statics> statics)
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
        private List<StaticsByDate> GetStaticsByDates(List<Data.Statics.Statics> statics)
        {
            var val = statics.GroupBy(x => x.CreateTimeTime.Date).Select(x => new StaticsByDate
            {
                Date = x.Key.Date,
                Count = x.Count()
            }).OrderBy(x => x.Date).Reverse().ToList();
            return val;
        }

        #endregion
        #region Utilities

        private async Task<string> GetCountryName(string ip)
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
        #region Ctor
        public StaticsService(ApplicationDbContext db)
        {
            _db = db;
        }

        #endregion


    }
}