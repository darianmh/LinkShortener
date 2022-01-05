using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Data.Link;
using LinkShortener.Data.User;
using LinkShortener.Models.Link;
using LinkShortener.ViewModel.User;
using Newtonsoft.Json;

namespace LinkShortener.Classes.Mapper
{
    public static class Mapper
    {
        #region Link

        public static LinkItemModel ToItemModel(this Link link)
        {
            return new LinkItemModel()
            {
                CreateDateTime = link.CreateDateTime,
                Description = link.Description,
                LinkTitle = link.LinkTitle,
                MainLink = link.MainLink,
                ShortLink = link.ShortLink,
                TotalVisitCount = link.TotalVisitCount
            };
        }

        public static LinkViewModel ToModel(this Link item)
        {
            return Map<LinkViewModel>(item);
        }
        public static Link ToEntity(this LinkViewModel item)
        {
            return Map<Link>(item);
        }

        #endregion


        #region User

        //ApplicationUser
        public static ApplicationUserModel ToModel(this ApplicationUser item)
        {
            return Map<ApplicationUserModel>(item);
        }

        public static ApplicationUser ToEntity(this ApplicationUserModel item)
        {
            return Map<ApplicationUser>(item);
        }

        //ApplicationRole
        public static ApplicationRoleModel ToModel(this ApplicationRole item)
        {
            return Map<ApplicationRoleModel>(item);
        }

        public static ApplicationRole ToEntity(this ApplicationRoleModel item)
        {
            return Map<ApplicationRole>(item);
        }


        //RoleAccess
        public static RoleAccessModel ToModel(this RoleAccess item)
        {
            return Map<RoleAccessModel>(item);
        }

        public static RoleAccess ToEntity(this RoleAccessModel item)
        {
            return Map<RoleAccess>(item);
        }

        //UserRoles
        public static UserRolesModel ToModel(this UserRoles item)
        {
            return Map<UserRolesModel>(item);
        }

        public static UserRoles ToEntity(this UserRolesModel item)
        {
            return Map<UserRoles>(item);
        }
        #endregion


        private static T Map<T>(object obj)
        {
            var txt = JsonConvert.SerializeObject(obj);
            var final = JsonConvert.DeserializeObject<T>(txt);
            return final;
        }
    }
}
