using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LinkShortener.Classes;
using LinkShortener.Data;
using LinkShortener.Data.User;
using LinkShortener.Services.Helper;
using LinkShortener.Services.Main;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Services.User
{
    public class RoleAccessService : MainService<RoleAccess>, IRoleAccessService
    {
        #region Fields


        #endregion
        #region Methods

        public async Task<List<SelectListItem>> GetRoleAccesses(int roleId)
        {
            var all = GetAllControllers();
            var roleAccesses = await GetRoleAccessesByRole(roleId);
            var accessIds = roleAccesses.Select(x => x.AttrName).ToList();
            return all.Select(x => new SelectListItem(x, x, accessIds.Contains(x))).ToList();
        }


        public async Task<List<RoleAccess>> GetRoleAccessesByRole(int roleId)
        {
            return await Queryable.Where(x => x.RoleId == roleId).ToListAsync();
        }

        public async Task SetRoleAccesses(int roleId, List<string> selectedRoleAccesses)
        {
            var last = await GetRoleAccessesByRole(roleId);
            await DeleteAsync(last);
            await CreateRoleAccesses(roleId, selectedRoleAccesses);
        }

        public async Task<List<RoleAccess>> GetAllAccessesByRoles(List<ApplicationRole> rolesModel)
        {
            var rolesId = rolesModel.Select(x => x.Id);
            var all = await Queryable.Where(x => rolesId.Contains(x.RoleId)).ToListAsync();
            all = all.GroupBy(x => x.AttrName).Select(x => x.First()).ToList();
            return all;
        }

        public async Task DeleteAllByRoleIdASync(int id)
        {
            var all = await GetRoleAccessesByRole(id);
            await DeleteAllAsync(all);
        }

        #endregion
        #region Utilities

        private async Task CreateRoleAccesses(int roleId, List<string> selectedRoleAccesses)
        {
            var result = new List<RoleAccess>();
            if (selectedRoleAccesses == null) return;
            foreach (var selectedRoleAccess in selectedRoleAccesses)
            {
                var temp = new RoleAccess()
                {
                    AttrName = selectedRoleAccess,
                    RoleId = roleId
                };
                result.Add(temp);
            }

            await InsertAsync(result);
        }

        /// <summary>
        /// دریافت نام کنترلر های پنل ادمین
        /// </summary>
        /// <returns></returns>
        private List<string> GetAllControllers()
        {
            var adminControllers = AssemblyHelper.AdminControllers;
            return GetControllersName(adminControllers);
        }

        private List<string> GetControllersName(List<Type> adminControllers)
        {
            var result = new List<string>();
            foreach (var controller in adminControllers)
            {
                var attr = controller.GetCustomAttribute(typeof(AdminFilterNameAttribute));
                if (attr == null)
                {
                    result.Add(controller.Name);
                    continue;
                }

                result.Add(((AdminFilterNameAttribute)attr).AccessName);
            }

            return result;
        }
        #endregion
        #region Ctor
        public RoleAccessService(ApplicationDbContext db) : base(db)
        {
        }
        #endregion


    }
}