using System;

namespace Data.LinkShortener.Classes
{
    /// <summary>
    /// set editor mode to hidden in admin panel edit template and fill from enum type
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class HiddenAttribute : Attribute
    {
        public HiddenAttribute()
        {
        }
    }
}