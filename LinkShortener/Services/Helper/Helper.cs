using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Services.Helper
{
    public static class Helper
    {
        #region Fields


        #endregion
        #region Methods
        /// <summary>
        /// get random string
        /// can not create bigger than 32 chars
        /// minimum length is 0 and max is 32 chars
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length = 5)
        {
            //new guid
            var temp = Guid.NewGuid().ToString("N");
            //if smaller than 0 return null
            if (length <= 0) return String.Empty;
            //between 0 and 32 takes from GUID
            if (length <= 32) return string.Join(string.Empty, temp.Take(length));
            //bigger than 32 return 32 chars
            return temp;
        }

        /// <summary>
        /// check that string is a valid link
        /// </summary>
        public static bool CheckLink(string link)
        {
            CheckHttp(ref link);
            Uri uriResult;
            bool result = Uri.TryCreate(link, UriKind.Absolute, out uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }


        #endregion
        #region Utilities

        /// <summary>
        /// check link has Http or Https
        /// </summary>
        /// <param name="link"></param>
        private static void CheckHttp(ref string link)
        {
            //if starts with Http or Https do nothing
            if (link.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || link.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                return;
            //else add http
            link = "http://" + link;
        }

        #endregion
        #region Ctor

        #endregion

    }
}
