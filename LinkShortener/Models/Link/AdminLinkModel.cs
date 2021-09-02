using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models.Link
{
    public class AdminLinkModel : BaseLinkModel
    {
        public AdminLinkModel(BaseLinkModel baseModel) : base(baseModel)
        {
        }

        public int VisitCount { get; set; }
    }
}
