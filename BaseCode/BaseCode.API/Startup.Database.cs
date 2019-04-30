using BaseCode.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaseCode.API
{
    public partial class Startup
    {
        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<BaseCodeEntities>(
            options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    optionsAction => { }
                )
            );

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<BaseCodeEntities>()
                    .AddDefaultTokenProviders();
        }
    }
}