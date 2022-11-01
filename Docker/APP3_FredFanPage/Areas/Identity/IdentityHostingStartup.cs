using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using APP3_FredFanPage.Areas.Identity;
using APP3_FredFanPage.Areas.Identity.Data;
using APP3_FredFanPage.Data;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace APP3_FredFanPage.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(
                (context, services) =>
                {
                    services.AddDbContext<FredFanPageContext>(
                        options =>
                            options.UseSqlite(
                                context.Configuration.GetConnectionString("FredFanPageContextConnection")));

                    services.AddDefaultIdentity<FredFanPageUser>(options => options.SignIn.RequireConfirmedAccount = true)
                        .AddEntityFrameworkStores<FredFanPageContext>();
                });
        }
    }
}