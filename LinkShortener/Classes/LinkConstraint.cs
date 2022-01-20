using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.LinkShortener.Data.Link;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Routing;
using Services.LinkShortener.Services.LinkService;
using Services.LinkShortener.Services.Statics;

namespace LinkShortener.Classes
{
    public class LinkConstraint : IRouteConstraint
    {

        /// <summary>
        /// check the inserted link =>
        /// if link exist in db, returns true
        /// else return false
        /// </summary>
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (httpContext == null) return false;
            //check the key exist
            if (values.TryGetValue(routeKey, out object rout))
            {
                //convert object key to string
                var routString = Convert.ToString(rout);
                if (!string.IsNullOrEmpty(routString))
                {
                    var checkStatic = false;
                    routString = CheckStaticInfo(routString, ref checkStatic);
                    //get service from context
                    var linkService = (ILinkService)httpContext.RequestServices.GetService(typeof(ILinkService));
                    var staticService = (IStaticsService)httpContext.RequestServices.GetService(typeof(IStaticsService));
                    if (linkService == null) return false;
                    //get link model if exist
                    var link = linkService.GetByShortLink(routString);
                    if (link != null)
                    {
                        //put link temp data for transfer to controller
                        httpContext.Items.TryAdd("LinkObject", link);
                        httpContext.Items.TryAdd("CheckStatic", checkStatic);
                        if (!checkStatic)
                        {
                            //log link visit
                            CreateVisitLog(link, httpContext, linkService, staticService);
                        }

                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// get and save client ip address that visited the link
        /// </summary>
        /// <param name="link"></param>
        /// <param name="httpContext"></param>
        /// <param name="linkService"></param>
        /// <param name="staticService"></param>
        private void CreateVisitLog(Link link, HttpContext httpContext, ILinkService linkService,
            IStaticsService staticService)
        {
            //update link
            UpdateLinkVisitCount(link, linkService);

            //add visit log record to db
            AddStaticLog(staticService, link, httpContext);
        }

        private void AddStaticLog(IStaticsService staticService, Link link, HttpContext httpContext)
        {
            var result = staticService?.Insert(link.ShortLink, httpContext.Connection.RemoteIpAddress?.ToString(), httpContext.Request.Headers["Referer"].ToString());
            while (result != null && !result.IsCompleted)
            {

            }
        }

        /// <summary>
        /// increase and update link visit count
        /// </summary>
        /// <param name="link"></param>
        /// <param name="linkService"></param>
        private void UpdateLinkVisitCount(Link link, ILinkService linkService)
        {
            link.TotalVisitCount += 1;
            linkService.Update(link);
        }

        /// <summary>
        /// check rout contains $ or not
        /// </summary>
        /// <param name="routString"></param>
        /// <param name="checkStatic"></param>
        /// <returns></returns>
        private string CheckStaticInfo(string routString, ref bool checkStatic)
        {
            if (routString.Contains('$'))
            {
                checkStatic = true;
                routString = routString.Replace("$", String.Empty);
            }

            return routString;
        }
    }
}
