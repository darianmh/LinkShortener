using System.Collections.Generic;

namespace LinkShortener.ViewModel
{

    /// <summary>
    /// این کلاس برای صفحه ویرایش یا ایجاد که به صورت جامع تولید می شود مدل را پاس می دهد
    /// </summary>
    public class EditCreateTemplateViewModel
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public bool IsRequired { get; set; }
        public string DisplayName { get; set; }
        public object Value { get; set; }
        public EditCreateTemplateType ObjectType { get; set; }
        /// <summary>
        /// لیست رمانی مقدار میگیرد که نوع مدل اینام یا دی بی لیست باشد
        /// </summary>
        public List<MySelectListItem> ListForOptionList { get; set; }
    }
}
