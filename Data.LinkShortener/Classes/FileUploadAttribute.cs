using System;

namespace Data.LinkShortener.Classes
{
    /// <summary>
    /// to set file upload for field in admin edit template
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FileUploadAttribute : Attribute
    {
        public bool Multiple { get; }

        public FileUploadAttribute(bool multiple = false)
        {
            Multiple = multiple;
        }
    }
}