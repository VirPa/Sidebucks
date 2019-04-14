using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sidebucks.BLL.v1.ConfigServices {
    public static class AuthenticationConfigService {
        public static IServiceCollection RegisterAuthentication(
            this IServiceCollection services) {

            services.AddAuthentication()
                .AddJwtBearer(option => {
                    option.RequireHttpsMetadata = false;
                    option.SaveToken = true;

                    option.TokenValidationParameters = new TokenValidationParameters {
                        ValidIssuer = "http://virpa.technology",
                        ValidAudience = "http://virpa.technology",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0987654321RUSH_B"))
                    };

                    //Note: this event is to store the active token in "access_token" key holder.
                    option.Events = new JwtBearerEvents {

                        OnTokenValidated = context => {

                            var accessToken = (JwtSecurityToken)context.SecurityToken;
                            if (accessToken == null) return Task.CompletedTask;
                            var identity = (ClaimsIdentity)context.Principal.Identity;
                            identity?.AddClaim(new Claim("access_token", accessToken.RawData));

                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}
