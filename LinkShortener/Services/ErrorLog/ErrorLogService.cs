using System;
using LinkShortener.Data;
using LinkShortener.Services.Main;

namespace LinkShortener.Services.ErrorLog
{
    public class ErrorLogService : MainService<Data.ErrorLog>, IErrorLogService
    {
        #region Fields

        private readonly ApplicationDbContext _db;


        #endregion
        #region Methods

        public void Insert(string text, string innerText)
        {
            var model = new Data.ErrorLog
            {
                InnerText = innerText,
                Text = text
            };
            Insert(model);
        }

        #endregion
        #region Utilities


        #endregion
        #region Ctor

        public ErrorLogService(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        #endregion

    }
}