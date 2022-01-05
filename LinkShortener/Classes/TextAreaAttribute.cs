using System;

namespace BlogAndShop.Data.Classes
{
    /// <summary>
    /// set editor mode to TextArea in admin panel edit template and fill from enum type
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TextAreaAttribute : Attribute
    {
        public TextAreaAttribute()
        {
        }
    }
}