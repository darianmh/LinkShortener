using System;
using System.ComponentModel.DataAnnotations;
using LinkShortener.ViewModel;

namespace LinkShortener.Models
{
    /// <summary>
    /// every item that inherits this class, will register in data base
    /// </summary>

    public class BaseEntity<TId>
    {
        [Key]
        public TId Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }


        public virtual MySelectListItem GetSelectListItem(string selected)
        {
            return new MySelectListItem(Id.ToString(), Id.ToString(), selected: Id.ToString().Equals(selected, StringComparison.OrdinalIgnoreCase));
        }
        public virtual string GetShowTextById(string id)
        {
            return id.ToString();
        }
        public virtual bool Find(string id)
        {
            return Id.ToString().Equals(id, StringComparison.OrdinalIgnoreCase);
        }

    }
    public class BaseEntity : BaseEntity<int>
    {
    }
}