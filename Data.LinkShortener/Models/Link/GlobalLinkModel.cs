namespace Data.LinkShortener.Models.Link
{
    public class GlobalLinkModel : LinkRedirectModel
    {
        public GlobalLinkModel(LinkRedirectModel link) : base(link)
        {

        }

        public GlobalLinkModel()
        {

        }

        public string MetaTitleModel { get; set; }
    }
}