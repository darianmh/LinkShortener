using System.Threading.Tasks;
using LinkShortener.Data.User;
using LinkShortener.Services.Main;

namespace LinkShortener.Services.User
{
    public interface IUserTokenService : IMainService<UserToken>
    {
        /// <summary>
        /// get UserToken in db if exist
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        Task<UserToken> GetUserToken(int userId, UserTokenType tokenType);
        /// <summary>
        /// check UserToken is exist in db and is valid
        /// tokens are valid for 3 minutes
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckUserToken(int userId, UserTokenType tokenType, string token);
        /// <summary>
        /// create new UserToken or return if is valid but will update time
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        Task<string> GenerateUserToken(int userId, UserTokenType tokenType);

        Task RemoveUserToken(int userId, UserTokenType resetPassword);
    }
}