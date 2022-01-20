using System.ComponentModel.DataAnnotations.Schema;
using Data.LinkShortener.Models;

namespace Data.LinkShortener.Data.User
{
    public class UserToken : BaseEntity
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public UserTokenType UserTokenType { get; set; }
        public string Token { get; set; }

        //np
        public virtual ApplicationUser User { get; set; }
    }
}
