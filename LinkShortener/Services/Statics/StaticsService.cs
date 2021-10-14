using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LinkShortener.Data;
using LinkShortener.Models.IpLocation;
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