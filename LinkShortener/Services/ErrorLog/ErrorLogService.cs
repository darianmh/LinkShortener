using System;
using LinkShortener.Data;

namespace LinkShortener.Services.ErrorLog
{
    public class ErrorLogService : IErrorLogService
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
                DateTime = DateTime.Now,
                Text = text
            };
            _db.ErrorLogs.Add(model);
            _db.SaveChanges();
        }

        #endregion
        #region Utilities


        #endregion
        #region Ctor

        public ErrorLogService(ApplicationDbContext db)
        {
            _db = db;
        }
        #endregion

    }
}