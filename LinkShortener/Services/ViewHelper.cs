using System;
using System.Linq;
using System.Reflection;
using LinkShortener.Classes;
using LinkShortener.Services.Helper;
using Microsoft.AspNetCore.Mvc.Razor;

namespace LinkShortener.Services
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
