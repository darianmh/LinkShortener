using System.ComponentModel.DataAnnotations.Schema;
using Data.LinkShortener.Data.User;


namespace Data.LinkShortener.ViewModel.User
{
    public class UserTokenModel : BaseEntityModel
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public UserTokenType UserTokenType { get; set; }
        public string Token { get; set; }

        //np
        public virtual ApplicationUserModel User { get; set; }
    }

}
