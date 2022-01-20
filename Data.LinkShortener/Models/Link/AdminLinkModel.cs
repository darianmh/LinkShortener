using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.LinkShortener.Models.Link
{
    public class AdminLinkModel : BaseLinkModel
    {
        public AdminLinkModel(BaseLinkModel baseModel) : base(baseModel)
        {
        }

        public int VisitCount { get; set; }
    }
}
