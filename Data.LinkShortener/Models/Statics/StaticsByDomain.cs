using System.Collections.Generic;

namespace Data.LinkShortener.Models.Statics
{
    /// <summary>
    /// shows click counts by domain
    /// </summary>
    public class StaticsByDomain
    {
        public string Domain { get; set; }
        public List<StaticsByUrl> Urls { get; set; }
        public int Count { get; set; }

    }
}