using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Data;
using Microsoft.EntityFrameworkCore;

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
                RefererUrl = refererUrl
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


        #endregion
        #region Ctor
        public StaticsService(ApplicationDbContext db)
        {
            _db = db;
        }

        #endregion


    }
}