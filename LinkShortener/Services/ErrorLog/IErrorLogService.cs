using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Services.ErrorLog
{
    public interface IErrorLogService
    {
        void Insert(string text, string innerText);
    }
}
