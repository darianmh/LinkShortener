using System;

namespace LinkShortener.Services.Main
{
    public static class Utilities
    {
        public static int GetRandomNumber(int length = 5)
        {
            var r = new Random();
            var num = r.Next(0, Convert.ToInt32(Math.Pow(10, length)));
            return num;
        }

        public static string GetUrlEncoded(string url)
        {
            return url;
        }

        public static string GetShortString(string metaDescription, int length)
        {
            var temp = metaDescription != null && metaDescription.Length > 150 ? metaDescription.Substring(0, 150) : metaDescription;
            return temp;
        }
    }
}
