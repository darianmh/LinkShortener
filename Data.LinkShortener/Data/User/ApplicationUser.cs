using System;
using System.Collections.Generic;
using System.Linq;
using Data.LinkShortener.ViewModel;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Data.LinkShortener.Data.User
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Family { get; set; }



        //np
        [Newtonsoft.Json.JsonIgnore]
        public virtual List<UserToken> UserTokens { get; set; }

        public bool IsSuperAdmin { get; set; }


        public MySelectListItem GetSelectListItem(string selected)
        {
            try
            {
                //اگر لیستی از محصولات باشد
                var allSelected = (List<int>)JsonConvert.DeserializeObject(selected);

                return new MySelectListItem(Email, Id.ToString(), selected: allSelected.Any(x => Id.ToString().Equals(x.ToString(), StringComparison.OrdinalIgnoreCase)));
            }
            catch
            {
                //اگر تک باشد
                return new MySelectListItem(Email, Id.ToString(), selected: Id.ToString().Equals(selected.ToString(), StringComparison.OrdinalIgnoreCase));
            }
        }

        public string GetShowTextById(string id)
        {
            return UserName;
        }

        public virtual bool Find(string id)
        {
            return Id.ToString().Equals(id, StringComparison.OrdinalIgnoreCase);
        }

    }
}