using Data.LinkShortener.Data;
using Services.LinkShortener.Services.Main;

namespace Services.LinkShortener.Services.ErrorLog
{
    public class ErrorLogService : MainService<Data.LinkShortener.Data.ErrorLog>, IErrorLogService
    {
        #region Fields

        private readonly ApplicationDbContext _db;


        #endregion
        #region Methods

        public void Insert(string text, string innerText)
        {
            var model = new Data.LinkShortener.Data.ErrorLog
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