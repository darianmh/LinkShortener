using System.Collections.Generic;

namespace Data.LinkShortener.ViewModel
{
    public class AdminPanelLinkGroup
    {
        public string Group { get; set; }
        public bool IsActive { get; set; }
        public List<AdminPanelLink> Links { get; set; }
    }
}