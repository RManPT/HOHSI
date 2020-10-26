using HOHSI.Areas.Identity.Data;
using HOHSI.Data;
using HOHSI.Models.Interfaces;
using HOHSI.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HOHSI
{
    public class Startup
    {
        //gets configuration for config files in order: appsettings.json, user secrets, environmental variables, CLI args
        //each config source overrides the previous one
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //configure rgdp
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<HOHSIContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("HOHSIContextConnection")));
            services.AddDefaultIdentity<HOHSIUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<HOHSIContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            //Repository pattern: connecting interfaces with implementations
            services.AddTransient<IExerciseRepository, ExerciseRepository>();
            services.AddTransient<IPrescriptionRepository, PrescriptionRepository>();
            services.AddTransient<IPrescriptedExerciseRepository, PrescriptedExerciseRepository>();
            //adding support for MVC
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Middleware pipeline: logging, static files(wwwroot), MVC
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //deals with bad urls
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home";
                    await next();
                }
            });
            //enforces RGDP
            app.UseCookiePolicy();
            // Enforce HTTPS in ASP.NET Core by redirecting HTTP requests to HTTPS
            app.UseHttpsRedirection();
            // allows wwwroot files to be accessed
            app.UseStaticFiles();

            // Routing is responsible for matching incoming HTTP requests and dispatching those requests to the app's executable endpoints. 1
            app.UseRouting();
            // Enforces authetication
            app.UseAuthentication();
            // Enforces authorization (roles)
            app.UseAuthorization();

            // Ensures DB is up to date and seeded
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<HOHSIContext>();
                try
                {
                    context.Database.Migrate();
                }
                catch
                {
                    Console.WriteLine("already migrated");
                }
                context.EnsureDBSeeded();
            }

            // Establishes endpoint patterns
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}