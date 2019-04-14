using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sidebucks.DAL.v1.Identity;

namespace Sidebucks.BLL.v1.ConfigServices {
    public static class IdentityConfigService {
        public static IServiceCollection RegisterIdentities(
            this IServiceCollection services, string dbConnection) {

            //Identity Database Tables
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(dbConnection));

            //Identity Configuration
            services.AddIdentity<ApplicationUser, IdentityRole>(option => {
                    option.Password.RequireDigit = false;
                    option.Password.RequireLowercase = false;
                    option.Password.RequireUppercase = false;
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<RoleManager<IdentityRole<int>>>();

            services.AddScoped<UserManager<ApplicationUser>>();

            return services;
        }
    }
}
