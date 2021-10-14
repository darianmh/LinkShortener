using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinkShortener.Services.Statics
{
    public interface IStaticsService
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
    }
}