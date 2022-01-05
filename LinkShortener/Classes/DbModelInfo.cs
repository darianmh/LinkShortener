using System.Collections.Generic;

namespace LinkShortener.Classes
{
    public class DbModelInfo<T>
    {
        public List<T> List { get; set; }
        public int TotalCount { get; set; }
    }
}