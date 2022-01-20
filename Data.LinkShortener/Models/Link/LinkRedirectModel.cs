namespace Data.LinkShortener.Models.Link
{
    public class LinkRedirectModel : BaseLinkModel
    {
        protected LinkRedirectModel(LinkRedirectModel link) : base(link)
        {
            this.Header = link.Header;
        }

        public LinkRedirectModel()
        {

        }

        public LinkRedirectModel(BaseLinkModel link) : base(link)
        {

        }

        public string Header { get; set; }

    }
}