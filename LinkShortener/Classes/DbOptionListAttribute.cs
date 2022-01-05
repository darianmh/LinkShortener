using System;

namespace BlogAndShop.Data.Classes
{
    /// <summary>
    /// set editor mode to OptionList in admin panel edit template and fill from db
    /// </summary>
    public class DbOptionListAttribute : Attribute
    {
        /// <summary>
        /// type of navigation property in database
        /// </summary>
        public Type NavigationProperty { get; set; }

        public bool AllowNull { get; set; }
        public bool Multiple { get; set; }
        public DbOptionListAttribute(Type np, bool allowNull, bool multiple = false)
        {
            NavigationProperty = np;
            AllowNull = allowNull;
            Multiple = multiple;
        }
    }
}