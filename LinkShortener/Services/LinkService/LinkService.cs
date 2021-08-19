using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Data;
using LinkShortener.Data.Link;
using LinkShortener.Models.Link;
using LinkShortener.Services.Statics;
using LinkShortener.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Services.LinkService
{
    public class LinkService : ILinkService
    {
        #region Fields

        private readonly ApplicationDbContext _db;
        private readonly ApplicationUserManager _applicationUserManager;


        #endregion
        #region Methods

        /// <inheritdoc/>
        public async Task<List<Link>> GetAllLinksAsync()
        {
            return await _db.Links.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Link> Create(string mainLink, HttpContext httpContext, int length = 4)
        {
            mainLink = Correct(mainLink);
            //this causes problems in case sensitive problem like 'https://hbr.org/2012/11/the-management-century'
            //mainLink = mainLink.ToUpper();
            try
            {
                var model = new Link()
                {
                    CreateDateTime = DateTime.Now,
                    MainLink = mainLink,
                    ShortLink = await GetNewShortLink(length),
                    IpV4 = GetIpV4(httpContext),
                    UserId = GetUserId(httpContext)
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

        /// <inheritdoc/>
        public async Task<List<AdminLinkModel>> ToAdminLinkModel(List<Link> links, IStaticsService staticsService)
        {
            var tempList = new List<AdminLinkModel>();
            foreach (var link in links)
            {
                var temp = await ToAdminLinkModel(link, staticsService);
                if (temp != null) tempList.Add(temp);
            }
            return tempList;
        }

        /// <inheritdoc/>
        public async Task<AdminLinkModel> ToAdminLinkModel(Link link, IStaticsService staticsService)
        {
            var model = new AdminLinkModel()
            {
                MainLink = link.MainLink,
                CreateDateTime = link.CreateDateTime,
                ShortLink = link.ShortLink,
                VisitCount = await GetVisitCount(link, staticsService)
            };
            return model;
        }


        #endregion
        #region Utilities
        /// <summary>
        /// if user is logged in with put user id in db data
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private int? GetUserId(HttpContext httpContext)
        {
            return _applicationUserManager.GetUserId(httpContext.User);
        }
        /// <summary>
        /// set user ip address for later usage
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private string GetIpV4(HttpContext httpContext)
        {
            return httpContext.Connection.RemoteIpAddress?.ToString();
        }

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

        /// <summary>
        /// get visit count from IStaticsService
        /// </summary>
        /// <param name="link"></param>
        /// <param name="staticsService"></param>
        /// <returns></returns>
        private async Task<int> GetVisitCount(Link link, IStaticsService staticsService)
        {
            var statics = await staticsService.GetStatics(link.ShortLink);
            return statics?.Count ?? 0;
        }

        #endregion
        #region Ctor

        public LinkService(ApplicationDbContext db, ApplicationUserManager applicationUserManager)
        {
            _db = db;
            _applicationUserManager = applicationUserManager;
        }
        #endregion

    }
}