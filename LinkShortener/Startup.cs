using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Data.LinkShortener.Data;
using Data.LinkShortener.Data.User;
using LinkShortener.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Services.LinkShortener.Services;
using Services.LinkShortener.Services.Helper;
using Services.LinkShortener.Services.User;

namespace LinkShortener
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment _webHostEnvironment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            string connectionString;
            if (_webHostEnvironment.IsDevelopment())
            {
                connectionString = Configuration.GetConnectionString("DevelopmentDefaultConnection");
            }
            else
            {
                connectionString = Configuration.GetConnectionString("ProductionDefaultConnection");
            }

            //add db
            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(connectionString, b => b.MigrationsAssembly("LinkShortener")));
            //add services
            services.AddTransient<UpdateDbService>();
            services.AddServices();
            //identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddRoleStore<ApplicationRoleStore>()
                .AddUserStore<ApplicationUserStore>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSigninManager>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //identity account
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                //options.Cookie.Expiration

                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/Account/Index";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                //options.ReturnUrlParameter=""
            });
            //assembly
            AssemblyHelper.BaseSiteAssembly = Assembly.GetExecutingAssembly();
            //admin controllers
            AdminPanelService.GetAllController(AssemblyHelper.BaseSiteAssembly);

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


            app.UseAuthentication();
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
