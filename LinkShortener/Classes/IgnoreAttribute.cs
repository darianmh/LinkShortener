using System;

namespace LinkShortener.Classes
{
    /// <summary>
    /// to ignore field in admin edit template
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreAttribute : Attribute
    {

    }
}