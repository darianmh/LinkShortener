using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        /// create new model and inserts in db
        /// </summary>
        /// <param name="mainLink">main link url</param>
        /// <param name="httpContext"></param>
        /// <param name="length">length of short link between 0 and 32 chars</param>
        /// <returns>link model if success and null if fail</returns>
        Task<Link> Create(string mainLink, HttpContext httpContext, int length = 5);
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
    }
}
