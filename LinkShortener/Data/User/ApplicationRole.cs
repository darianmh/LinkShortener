using System;
using System.Collections.Generic;
using System.Linq;
using LinkShortener.ViewModel;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LinkShortener.Data.User
{
    public class ApplicationRole : IdentityRole<int>
    {
        //np
        [JsonIgnore]
        public virtual List<RoleAccess> RoleAccesses { get; set; }


        public MySelectListItem GetSelectListItem(string selected)
        {
            var array = (JArray)JsonConvert.DeserializeObject(selected);
            var selectedValues = array != null ? array.Select(Convert.ToInt32).ToList() : new List<int>();
            return new MySelectListItem(Name, Id.ToString(), selectedValues.Contains(Id));
        }
    }
}