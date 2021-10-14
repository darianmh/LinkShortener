using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using LinkShortener.Classes;
using LinkShortener.Data;
using LinkShortener.Data.User;
using LinkShortener.Services.ErrorLog;
using LinkShortener.Services.LinkService;
using LinkShortener.Services.Statics;
using LinkShortener.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //add db
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("LinkConnectionString")));
            //add services
            services.AddTransient<ILinkService, LinkService>();
            services.AddTransient<IStaticsService, StaticsService>();
            services.AddTransient<IErrorLogService, ErrorLogService>();
            //identity service
            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddRoles<ApplicationRole>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>();

            //unicode
            services.AddSingleton<HtmlEncoder>(
                HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
                    UnicodeRanges.Arabic }));

            //routing services (registering LinkConstraint)
            services.Configure<RouteOptions>(routeOptions =>
            {
                routeOptions.ConstraintMap.Add("links", typeof(LinkConstraint));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Home/Error?code={0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //link shortener
                endpoints.MapControllerRoute(
                    name: "links",
                    constraints: new { links = new LinkConstraint() },
                    pattern: "{*links}",
                    defaults: new { controller = "Rout", action = "Index" });
                ////default
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
