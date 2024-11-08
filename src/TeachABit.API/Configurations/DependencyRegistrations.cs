using Microsoft.AspNetCore.Identity;
using TeachABit.Model.Models.User;
using TeachABit.Repository.Repositories.Radionice;
using TeachABit.Repository.Repositories.Tecajevi;
using TeachABit.Service.Services.Authentication;
using TeachABit.Service.Services.Authorization;
using TeachABit.Service.Services.Radionice;
using TeachABit.Service.Services.Tecajevi;
using TeachABit.Service.Util.Mail;
using TeachABit.Service.Util.Token;

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
            services.AddScoped<IMailSenderService, MailSenderService>();
            services.AddScoped<ITecajeviRepository, TecajeviRepository>();
            services.AddScoped<ITecajeviService, TecajeviService>();
            services.AddScoped<IRadioniceRepository, RadioniceRepository>();
            services.AddScoped<IRadioniceService, RadioniceService>();
            return services;
        }
    }
}
