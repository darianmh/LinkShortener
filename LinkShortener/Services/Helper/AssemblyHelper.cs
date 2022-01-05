using System;
using System.Collections.Generic;
using System.Reflection;

namespace LinkShortener.Services.Helper
{
    public static class AssemblyHelper
    {
        public static Assembly BaseSiteAssembly { get; set; }
        public static List<Type> AdminControllers { get; set; }
    }
}