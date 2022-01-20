using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Razor;
using Services.LinkShortener.Classes;
using Services.LinkShortener.Services.Helper;

namespace Admin.LinkShortener.Classes
{
    public static class ViewHelper
    {
        /// <summary>
        /// get attr that holds controller info and name
        /// </summary>
        /// <param name="pageBase"></param>
        /// <returns></returns>
        public static AdminFilterNameAttribute GetInfo(this RazorPageBase pageBase)
        {
            var controllerName = pageBase.ViewContext.RouteData?.Values["controller"]?.ToString();
            var type = AssemblyHelper.AdminControllers.FirstOrDefault(x => x.Name.Equals((controllerName + "Controller"), StringComparison.InvariantCultureIgnoreCase));
            var controllerInfo = (AdminFilterNameAttribute)type?.GetCustomAttribute(typeof(AdminFilterNameAttribute));
            return controllerInfo;
        }
    }
}
