using System;
using System.Threading.Tasks;
using Data.LinkShortener.Data;
using Data.LinkShortener.Data.User;
using Microsoft.EntityFrameworkCore;
using Services.LinkShortener.Services.Main;

namespace Services.LinkShortener.Services.User
{
    public class UserTokenService : MainService<UserToken>, IUserTokenService
    {
        #region Fields


        #endregion
        #region Methods

        public async Task<UserToken> GetUserToken(int userId, UserTokenType tokenType)
        {
            return await Queryable.FirstOrDefaultAsync(x => x.UserId == userId && x.UserTokenType == tokenType);
        }

        public async Task<bool> CheckUserToken(int userId, UserTokenType tokenType, string token)
        {
            var dbToken = await GetUserToken(userId, tokenType);
            return dbToken != null && dbToken.Token.Equals(token, StringComparison.OrdinalIgnoreCase) && dbToken.UpdateDateTime.AddMinutes(5) >= DateTime.Now;
        }

        public async Task<string> GenerateUserToken(int userId, UserTokenType tokenType)
        {
            //check if token is in db
            var token = await GetUserToken(userId, tokenType);
            if (token == null)
                token = await GenerateNewUserToken(userId, tokenType);
            else await UpdateAsync(token);
            return token.Token;
        }

        public async Task RemoveUserToken(int userId, UserTokenType tokenType)
        {
            var token = await GetUserToken(userId, tokenType);
            await DeleteAsync(token);
        }

        #endregion
        #region Utilities


        private async Task<UserToken> GenerateNewUserToken(int userId, UserTokenType tokenType)
        {
            var random = new Random();
            var num = random.Next(10000, 99999);
            var model = new UserToken()
            {
                UserId = userId,
                UserTokenType = tokenType,
                Token = num.ToString()
            };
            await InsertAsync(model);
            return model;
        }

        #endregion
        #region Ctor
        public UserTokenService(ApplicationDbContext db) : base(db)
        {
        }
        #endregion



    }
}