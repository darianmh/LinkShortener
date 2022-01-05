using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Services.Main;

namespace LinkShortener.Services.ErrorLog
{
    public interface IErrorLogService : IMainService<Data.ErrorLog>
    {
        void Insert(string text, string innerText);
    }
}
