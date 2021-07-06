using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Data.Link;

namespace LinkShortener.Services.LinkService
{
    public interface ILinkService
    {
        /// <summary>
        /// create new model and inserts in db
        /// </summary>
        /// <param name="mainLink">main link url</param>
        /// <param name="length">length of short link between 0 and 32 chars</param>
        /// <returns>link model if success and null if fail</returns>
        Task<Link> Create(string mainLink, int length = 5);
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
    }
}
