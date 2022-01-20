using System;

namespace Data.LinkShortener.Classes
{
    /// <summary>
    /// کلید برای ایندکس
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AdminKeyAttribute : Attribute
    {
        public AdminKeyAttribute()
        {
        }
    }
}