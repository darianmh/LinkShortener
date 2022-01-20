using System;

namespace Data.LinkShortener.Classes
{
    /// <summary>
    /// هر عضوی که این اتریبیوت را داشته باشد در لیست پنل ادمین نمایش داده می شود
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AdminShowItemAttribute : Attribute
    {
        public int Order { get; }
        public AdminShowItemAttribute(int order)
        {
            Order = order;
        }
    }
}