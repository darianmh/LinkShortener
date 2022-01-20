using System;

namespace Data.LinkShortener.Models.Statics
{
    /// <summary>
    /// shows click counts by month
    /// </summary>
    public class StaticsByMonth
    {
        public string Month { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
    }
}