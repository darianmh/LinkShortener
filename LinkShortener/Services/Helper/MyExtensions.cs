using System;
using System.Linq;
using LinkShortener.Data;

namespace LinkShortener.Services.Helper
{
    public static class MyExtensions
    {
        /// <summary>
        /// find generic type in db set
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IQueryable<object> Set(this ApplicationDbContext _context, Type t)
        {
            var type = _context.GetType();
            var methods = type.GetMethods();
            var method = methods.FirstOrDefault(x => x.Name == "Set");
            var generic = method?.MakeGenericMethod(t);
            var result = generic.Invoke(_context, null);
            return (IQueryable<object>)result;
        }



        public static string GetShortString(this string text, int length = 60)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return text.Length < length ? text : string.Join(String.Empty, text.Take(length)) + "...";
        }
    }
}