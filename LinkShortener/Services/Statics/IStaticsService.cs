using System.Collections.Generic;
using System.Threading.Tasks;
using LinkShortener.Data.Link;
using LinkShortener.Models.Statics;
using LinkShortener.Services.Main;

namespace LinkShortener.Services.Statics
{
    public interface IStaticsService : IMainService<Data.Statics.Statics>
    {
        /// <summary>
        /// instance insert without any checks
        /// </summary>
        Task Insert(string shortLink, string ip, string refererUrl);
        /// <summary>
        /// return all data whit short link address
        /// </summary>
        /// <returns>list of statics</returns>
        Task<List<Data.Statics.Statics>> GetStatics(string shortLink);

        /// <summary>
        /// create report from statics log
        /// </summary>
        /// <param name="statics"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        StaticModel GetStaticsModel(List<Data.Statics.Statics> statics, Link link);

        /// <summary>
        /// get country info from ip address
        /// https://ip-geolocation-ipwhois-io.p.rapidapi.com
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        Task<string> GetCountryName(string ip);

    }
}