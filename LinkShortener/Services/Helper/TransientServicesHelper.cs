using System.Linq;
using System.Reflection;
using LinkShortener.Data;
using LinkShortener.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace LinkShortener.Services.Helper
{
    public static class TransientServicesHelper
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            //sign in
            //services.AddTransient<ApplicationUserManager>();
            //services.AddTransient<ApplicationSignInManager>();

            //db
            services.AddTransient<ApplicationDbContext>();
            //services

            var allTypes = assembly.GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract).ToList();
            var interfaces = assembly.GetTypes()
                .Where(myType => myType.IsInterface).ToList();
            foreach (var type in interfaces)
            {
                var typesToRegister = allTypes.Where(z => type.IsAssignableFrom(z)).ToList();
                var typeToRegister = typesToRegister.FirstOrDefault();
                if (typeToRegister != null) services.AddTransient(type, typeToRegister);
            }




            //identity
            services.AddTransient<ApplicationUserManager>();
            services.AddTransient<ApplicationUserStore>();
            services.AddTransient<ApplicationRoleManager>();
            services.AddTransient<ApplicationRoleStore>();
            services.AddTransient<ApplicationSigninManager>();




            return services;
        }
    }
}
