using Services.LinkShortener.Services.Main;

namespace Services.LinkShortener.Services.ErrorLog
{
    public interface IErrorLogService : IMainService<Data.LinkShortener.Data.ErrorLog>
    {
        void Insert(string text, string innerText);
    }
}
