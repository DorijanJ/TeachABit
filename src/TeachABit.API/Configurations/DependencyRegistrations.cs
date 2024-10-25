using Microsoft.AspNetCore.Identity;
using TeachABit.Model.Models;
using TeachABit.Service.Services.Authentication;
using TeachABit.Service.Services.Authorization;
using TeachABit.Service.Util;

namespace TeachABit.API.Configurations
{
    public static class DependencyRegistrations
    {
        public static IServiceCollection RegisterDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<SignInManager<AppUser>>();
            services.AddScoped<UserManager<AppUser>>();
            return services;
        }
    }
}
