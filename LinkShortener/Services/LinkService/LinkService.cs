using System;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Data;
using LinkShortener.Data.Link;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Services.LinkService
{
    public class LinkService : ILinkService
    {
        #region Fields

        private readonly ApplicationDbContext _db;


        #endregion
        #region Methods
        /// <inheritdoc/>
        public async Task<Link> Create(string mainLink, int length = 5)
        {
            mainLink = Correct(mainLink);
            mainLink = mainLink.ToUpper();
            try
            {
                var model = new Link()
                {
                    CreateDateTime = DateTime.Now,
                    MainLink = mainLink,
                    ShortLink = await GetNewShortLink(length)
                };
                await _db.Links.AddAsync(model);
                await _db.SaveChangesAsync();
                return model;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <inheritdoc/>
        public async Task<bool> CheckShortLink(string shortLink)
        {
            shortLink = shortLink.ToUpper();
            return await _db.Links.AnyAsync(x => x.ShortLink.Equals(shortLink));
        }

        /// <inheritdoc/>
        public async Task<Link> GetByShortLinkAsync(string shortLink)
        {
            shortLink = shortLink.ToUpper();
            return await _db.Links.FirstOrDefaultAsync(x =>
                x.ShortLink.Equals(shortLink));
        }

        /// <inheritdoc/>
        public Link GetByShortLink(string shortLink)
        {
            shortLink = shortLink.ToUpper();
            return _db.Links.FirstOrDefault(x =>
               x.ShortLink.Equals(shortLink));
        }

        #endregion
        #region Utilities
        /// <summary>
        /// fix link format
        /// </summary>
        /// <param name="mainLink">link</param>
        /// <returns>fixed link</returns>
        private string Correct(string mainLink)
        {
            //add http
            if (!(mainLink.StartsWith("http:") || mainLink.StartsWith("https:")))
            {
                mainLink = "http://" + mainLink;
            }

            return mainLink;
        }

        /// <summary>
        /// generate random links and check they be unique
        /// </summary>
        /// <returns>unique random link</returns>
        private async Task<string> GetNewShortLink(int length = 5)
        {
            string lnk;
            bool check;
            do
            {
                //get new link
                lnk = Helper.Helper.RandomString(length).ToUpper();
                //check in db
                check = await CheckShortLink(lnk);
            }
            //will exit when link is unique ( if check is true that means link exists in db )
            while (check);
            return lnk;
        }

        #endregion
        #region Ctor

        public LinkService(ApplicationDbContext db)
        {
            _db = db;
        }
        #endregion

    }
}