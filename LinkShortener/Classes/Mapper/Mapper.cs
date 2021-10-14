using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Data.Link;
using LinkShortener.Models.Link;

namespace LinkShortener.Classes.Mapper
{
    public static class Mapper
    {
        #region Link

        public static LinkItemModel ToItemModel(this Link link)
        {
            return new LinkItemModel()
            {
                CreateDateTime = link.CreateDateTime,
                Description = link.Description,
                LinkTitle = link.LinkTitle,
                MainLink = link.MainLink,
                ShortLink = link.ShortLink,
                TotalVisitCount = link.TotalVisitCount
            };
        }

        #endregion
    }
}
