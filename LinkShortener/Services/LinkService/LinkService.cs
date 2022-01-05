using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using LinkShortener.Data;
using LinkShortener.Data.Link;
using LinkShortener.Models.Link;
using LinkShortener.Services.Helper;
using LinkShortener.Services.Main;
using LinkShortener.Services.Statics;
using LinkShortener.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Services.LinkService
{
    public class LinkService : MainServiceNonBaseEntity<Link>, ILinkService
    {
        #region Fields

        private readonly ApplicationDbContext _db;
        private readonly ApplicationUserManager _applicationUserManager;


        #endregion
        #region Methods

        /// <summary>
        /// fix link format
        /// </summary>
        /// <param name="mainLink">link</param>
        /// <returns>fixed link</returns>
        public static string Correct(string mainLink)
        {
            if (string.IsNullOrEmpty(mainLink)) return "";
            //add http
            if (!(mainLink.StartsWith("http:") || mainLink.StartsWith("https:")))
            {
                mainLink = "http://" + mainLink;
            }

            return mainLink;
        }
        /// <inheritdoc/>
        public async Task<List<Link>> GetAllLinksAsync()
        {
            return await _db.Links.ToListAsync();
        }

        public async Task<List<Link>> GetAllPublicLinksAsync()
        {
            return await _db.Links.Where(x => x.IsPublic).ToListAsync();
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
                    UserId = GetUserId(httpContext),
                    TotalVisitCount = 0,
                    //todo currently all links are public
                    IsPublic = true
                };
                model = await GetLinkInfo(model, mainLink);
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
        public async Task<Link> GetLinkInfo(Link model, string mainLink)
        {
            var header = await GetHttpHeader(mainLink);
            if (header == null) header = "";
            model.HeaderText = header;
            model.LinkTitle = GetLinkTitle(header);
            model.Description = GetLinkDescription(header);
            return model;
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
            var baseModel = ToBaseLinkModel(link);
            var model = new AdminLinkModel(baseModel);
            model.VisitCount = await GetVisitCount(link, staticsService);
            return model;
        }
        /// <inheritdoc/>
        public List<AdminLinkModel> ToAdminLinkModel(List<Link> links)
        {
            var tempList = new List<AdminLinkModel>();
            foreach (var link in links)
            {
                var temp = ToAdminLinkModel(link);
                if (temp != null) tempList.Add(temp);
            }
            return tempList;
        }

        /// <inheritdoc/>
        public AdminLinkModel ToAdminLinkModel(Link link)
        {
            var baseModel = ToBaseLinkModel(link);
            var model = new AdminLinkModel(baseModel);
            model.VisitCount = link.TotalVisitCount;
            return model;
        }

        public GlobalLinkModel ToGlobalLinkModel(Link link)
        {
            var model = new GlobalLinkModel(ToLinkRedirectModel(link));
            model.MetaTitleModel = link.LinkTitle ?? link.MainLink;
            return model;
        }

        public LinkRedirectModel ToLinkRedirectModel(Link link)
        {
            var model = new LinkRedirectModel(ToBaseLinkModel(link));
            model.Header = link.HeaderText;
            return model;
        }

        public List<GlobalLinkModel> ToGlobalLinkModel(List<Link> links)
        {
            var tempList = new List<GlobalLinkModel>();
            foreach (var link in links)
            {
                var temp = ToGlobalLinkModel(link);
                if (temp != null) tempList.Add(temp);
            }
            return tempList;
        }

        public ShowAllLinksModel<AdminLinkModel> GetAllLinksShowModelAsync(SortBy sortBy, SortType sortType,
            int pageNumber)
        {
            var links = GetSortedLinks(sortBy, sortType, ref pageNumber, out int allPagesCount);
            var adminLinks = ToAdminLinkModel(links);
            var model = CreateShowModel<AdminLinkModel>(sortBy, sortType, pageNumber, allPagesCount, adminLinks);
            return model;
        }



        public ShowAllLinksModel<GlobalLinkModel> GetGlobalLinksShowModelAsync(SortBy sortBy, SortType sortType,
            int pageNumber)
        {
            var links = GetSortedLinks(sortBy, sortType, ref pageNumber, out int allPagesCount);
            var adminLinks = ToGlobalLinkModel(links);
            var model = CreateShowModel<GlobalLinkModel>(sortBy, sortType, pageNumber, allPagesCount, adminLinks);
            return model;
        }

        public Link GetByShortLinkNoTrack(string shortLink)
        {
            shortLink = shortLink.ToUpper();
            return _db.Links.AsNoTracking().FirstOrDefault(x =>
               x.ShortLink.Equals(shortLink));
        }

        #endregion
        #region Utilities

        /// <summary>
        /// reads header meta title
        /// </summary>
        /// <returns></returns>
        private string GetLinkTitle(string header)
        {
            if (string.IsNullOrEmpty(header)) return "";
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(header);
            HtmlNode title = doc.DocumentNode.SelectSingleNode("//title");
            var text = title?.InnerText;
            return text;
        }
        /// <summary>
        /// reads header meta description
        /// </summary>
        /// <returns></returns>
        private string GetLinkDescription(string header)
        {
            if (string.IsNullOrEmpty(header)) return "";
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(header);
            var metas = doc.DocumentNode.SelectNodes("//meta");
            var description = metas.FirstOrDefault(x => x.GetAttributeValue("name", "") == "description");
            var text = description?.GetAttributeValue("content", "") ?? "";
            return text;
        }
        /// <summary>
        /// open url and get header tags text
        /// if response is null that means url is invalid
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetHttpHeader(string mainLink)
        {
            var html = await HttpHelper.HttpGet(mainLink);
            if (string.IsNullOrEmpty(html)) return null;
            var header = FindHeader(html);
            return header ?? "";
        }
        /// <summary>
        /// finds html header
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private string FindHeader(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            HtmlNode header = doc.DocumentNode.SelectSingleNode("//head");
            var text = header?.InnerHtml;
            return text;
        }

        /// <summary>
        /// creates model for passing to view
        /// </summary>
        /// <typeparam name="T">type of list to cast</typeparam>
        /// <param name="sortBy">date or visit count</param>
        /// <param name="sortType">asc or dsc</param>
        /// <param name="pageNumber">current page</param>
        /// <param name="allPagesCount">total pages count</param>
        /// <param name="links">all found links</param>
        /// <returns></returns>
        private ShowAllLinksModel<T> CreateShowModel<T>(SortBy sortBy, SortType sortType, in int pageNumber, in int allPagesCount, List<T> links)
        {
            var model = new ShowAllLinksModel<T>
            {
                AllLinks = links,
                SortModel = GetSortModel(sortBy, sortType, pageNumber, allPagesCount)
            };
            return model;
        }
        /// <summary>
        /// create sort model for passing to view
        /// </summary>
        /// <returns></returns>
        private ShowAllLinksSortModel GetSortModel(SortBy sortBy, SortType sortType, int pageNumber, int allPagesCount)
        {
            return new ShowAllLinksSortModel()
            {
                SortBy = sortBy,
                AllPages = allPagesCount,
                CurrentPage = pageNumber,
                SortType = sortType
            };
        }

        /// <summary>
        /// find all links and sort them by given values
        /// </summary>
        /// <returns></returns>
        private List<Link> GetSortedLinks(SortBy sortBy, SortType sortType, ref int pageNumber, out int allPages)
        {
            //first page check
            if (pageNumber < 1) pageNumber = 1;
            //links per page
            int pageLinkNumber = 10;
            //all links as queryable
            var all = _db.Links.AsQueryable();
            all = Sort(all, sortBy, sortType);
            all = Paging(all, ref pageNumber, pageLinkNumber, out allPages);
            return all.ToList();
        }
        /// <summary>
        /// reduce links number fot current page
        /// </summary>
        /// <returns></returns>
        private IQueryable<Link> Paging(IQueryable<Link> all, ref int pageNumber, int pageLinkNumber, out int allPages)
        {
            allPages = (all.Count() / pageLinkNumber) + 1;
            //last page check
            if (pageNumber > allPages) pageNumber = allPages;
            return all.Skip((pageNumber - 1) * pageLinkNumber).Take(pageLinkNumber);
        }
        /// <summary>
        /// sort for asc or dsc and  date or visit count
        /// </summary>
        /// <returns></returns>
        private IQueryable<Link> Sort(IQueryable<Link> all, SortBy sortBy, SortType sortType)
        {
            //sort by date or visit count
            all = sortBy == SortBy.Date ? all.OrderBy(x => x.CreateDateTime) : all.OrderBy(x => x.TotalVisitCount);
            //reverse list id type is dsc
            if (sortType == SortType.Descending)
                all = all.Reverse();
            return all;
        }


        /// <summary>
        /// creating base link model
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        private BaseLinkModel ToBaseLinkModel(Link link)
        {
            var model = new BaseLinkModel()
            {
                MainLink = link.MainLink,
                CreateDateTime = link.CreateDateTime,
                ShortLink = link.ShortLink,

            };
            return model;
        }
        /// <summary>
        /// if user is logged in with put user id in db data
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private int? GetUserId(HttpContext httpContext)
        {
            var id = _applicationUserManager.GetUserId(httpContext.User);
            return string.IsNullOrEmpty(id) ? (int?)null : Convert.ToInt32(id);
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

        public LinkService(ApplicationDbContext db, ApplicationUserManager applicationUserManager) : base(db)
        {
            _db = db;
            _applicationUserManager = applicationUserManager;
        }
        #endregion

    }
}