using System;
using HOHSI.Areas.Identity.Data;
using HOHSI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(HOHSI.Areas.Identity.IdentityHostingStartup))]
namespace HOHSI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
               /* services.AddDbContext<HOHSIContext>(options =>
                    options.UseMySql(
                        context.Configuration.GetConnectionString("HOHSIContextConnection")));

                services.AddDefaultIdentity<HOHSIUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<HOHSIContext>();*/
            });
        }
    }
}