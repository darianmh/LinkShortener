using System.Collections.Generic;
using System.Threading.Tasks;
using Data.LinkShortener.Data.User;

using Microsoft.AspNetCore.Mvc.Rendering;
using Services.LinkShortener.Services.Main;

namespace Services.LinkShortener.Services.User
{
    public interface IRoleAccessService : IMainService<RoleAccess>
    {
        /// <summary>
        /// دریافت لیست دسترسی ها
        /// به همراه دسترسی های موجود برای نقش
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<SelectListItem>> GetRoleAccesses(int roleId);

        /// <summary>
        /// دریافت دسترسی های نقش
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<RoleAccess>> GetRoleAccessesByRole(int roleId);

        /// <summary>
        /// حذف دسترسی قبلی و تنظیم دسترسی های جدید
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="selectedRoleAccesses"></param>
        /// <returns></returns>
        Task SetRoleAccesses(int roleId, List<string> selectedRoleAccesses);

        /// <summary>
        /// دریافت همه دسترسی ها بر اساس همه نقش ها
        /// </summary>
        /// <param name="rolesModel"></param>
        /// <returns></returns>
        Task<List<RoleAccess>> GetAllAccessesByRoles(List<ApplicationRole> rolesModel);

        Task DeleteAllByRoleIdASync(int id);
    }
}
