using System.Linq;
using System.Threading.Tasks;

namespace Data.LinkShortener.Models.Statics
{
    /// <summary>
    /// shows click counts by country
    /// </summary>
    public class StaticsByCountry
    {
        public string CountryName { get; set; }
        public int Count { get; set; }
    }
}
