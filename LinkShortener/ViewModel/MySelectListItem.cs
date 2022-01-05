using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkShortener.ViewModel
{
    public class MySelectListItem : SelectListItem
    {
        public MySelectListItem(string name, string value, in bool selected) : base(name, value, selected)
        {

        }
        public MySelectListItem(string name, string value, in bool selected, bool hasImage, string imagePath) : base(name, value, selected)
        {
            HasImage = hasImage;
            ImagePath = imagePath;
        }

        public MySelectListItem(string name, string value) : base(name, value)
        {

        }

        /// <summary>
        /// اگر می خواهیم در لیست عکس نمایش بدهیم
        /// </summary>
        public bool HasImage { get; set; }
        /// <summary>
        /// آدرس تصویر اگر عکس بخواهیم
        /// </summary>
        public string ImagePath { get; set; }
    }
}