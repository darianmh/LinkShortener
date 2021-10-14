using System;

namespace LinkShortener.Models.Statics
{
    /// <summary>
    /// shows click counts by date
    /// </summary>
    public class StaticsByDate
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}