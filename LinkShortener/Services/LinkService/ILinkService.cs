using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using LinkShortener.Data.Link;
using LinkShortener.Models.Link;
using LinkShortener.Services.Statics;
using Microsoft.AspNetCore.Http;

namespace LinkShortener.Services.LinkService
{
    public interface ILinkService
    {
        /// <summary>
        /// get all links
        /// </summary>
        /// <returns></returns>
        Task<List<Link>> GetAllLinksAsync();
        /// <summary>
        /// get all public links
        /// </summary>
        /// <returns></returns>
        Task<List<Link>> GetAllPublicLinksAsync();

        /// <summary>
        /// create new model and inserts in db
        /// </summary>
        /// <param name="mainLink">main link url</param>
        /// <param name="httpContext"></param>
        /// <param name="length">length of short link between 0 and 32 chars</param>
        /// <returns>link model if success and null if fail</returns>
        Task<Link> Create(string mainLink, HttpContext httpContext, int length = 5);

        /// <summary>
        /// main link header and title
        /// </summary>
        /// <param name="model"></param>
        /// <param name="mainLink"></param>
        /// <returns></returns>
        Task<Link> GetLinkInfo(Link model, string mainLink);
        /// <summary>
        /// checks short link exist
        /// case is not important
        /// </summary>
        /// <returns>lnk exist</returns>
        Task<bool> CheckShortLink(string shortLink);
        /// <summary>
        /// return link model found by short link address
        /// </summary>
        /// <param name="shortLink">short link</param>
        /// <returns>link model</returns>
        Task<Link> GetByShortLinkAsync(string shortLink);
        /// <summary>
        /// return link model found by short link address
        /// </summary>
        /// <param name="shortLink">short link</param>
        /// <returns>link model</returns>
        Link GetByShortLink(string shortLink);

        /// <summary>
        /// convert link db model to custom model for admin panel
        /// </summary>
        /// <param name="links"></param>
        /// <param name="staticsService"></param>
        /// <returns></returns>
        Task<List<AdminLinkModel>> ToAdminLinkModel(List<Link> links, IStaticsService staticsService);

        /// <summary>
        /// convert link db model to custom model for admin panel
        /// </summary>
        /// <param name="link"></param>
        /// <param name="staticsService"></param>
        /// <param name="links"></param>
        /// <returns></returns>
        Task<AdminLinkModel> ToAdminLinkModel(Link link, IStaticsService staticsService);
        /// <summary>
        /// convert link db model to custom model for admin panel
        /// </summary>
        /// <param name="links"></param>
        /// <returns></returns>
        List<AdminLinkModel> ToAdminLinkModel(List<Link> links);

        /// <summary>
        /// convert link db model to custom model for admin panel
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        AdminLinkModel ToAdminLinkModel(Link link);
        /// <summary>
        /// convert link db model to custom model for Global show
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        GlobalLinkModel ToGlobalLinkModel(Link link);

        /// <summary>
        /// convert link db model to custom model for link redirect show
        /// </summary>
        /// <param name="link"></param>
        /// <param name="linkInfoService"></param>
        /// <returns></returns>
        LinkRedirectModel ToLinkRedirectModel(Link link);
        /// <summary>
        /// convert link db model to custom model for Global show
        /// </summary>
        /// <returns></returns>
        List<GlobalLinkModel> ToGlobalLinkModel(List<Link> links);
        /// <summary>
        /// update link model to db
        /// </summary>
        /// <param name="link"></param>
        void Update(Link link);

        ShowAllLinksModel<AdminLinkModel> GetAllLinksShowModelAsync(SortBy sortBy, SortType sortType, int pageNumber);
        ShowAllLinksModel<GlobalLinkModel> GetGlobalLinksShowModelAsync(SortBy sortBy, SortType sortType,
            int pageNumber);
        /// <summary>
        /// find link with short link ask no tracking from db
        /// </summary>
        /// <param name="routString"></param>
        /// <returns></returns>
        Link GetByShortLinkNoTrack(string? routString);
    }
}
