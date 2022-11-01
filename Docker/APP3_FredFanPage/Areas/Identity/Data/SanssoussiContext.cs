using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using APP3_FredFanPage.Areas.Identity.Data;

namespace APP3_FredFanPage.Data
{
    public class FredFanPageContext : IdentityDbContext<FredFanPageUser>
    {
        public FredFanPageContext(DbContextOptions<FredFanPageContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}