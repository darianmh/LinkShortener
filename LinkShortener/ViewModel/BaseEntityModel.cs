using System;
using LinkShortener.Classes;

namespace LinkShortener.ViewModel
{
    /// <summary>
    /// every item that inherits this class, will register in data base
    /// </summary>
    public class BaseEntityModel : BaseEntityModel<int>
    {
    }
    public class BaseEntityModel<TId>
    {
        [AdminKey]
        public TId Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}