using System;

namespace Data.LinkShortener.Models.Link
{
    public class BaseLinkModel
    {
        protected BaseLinkModel(BaseLinkModel baseModel)
        {
            ShortLink = baseModel.ShortLink;
            MainLink = baseModel.MainLink;
            CreateDateTime = baseModel.CreateDateTime;
        }

        public BaseLinkModel()
        {

        }
        public string ShortLink { get; set; }
        public string MainLink { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}