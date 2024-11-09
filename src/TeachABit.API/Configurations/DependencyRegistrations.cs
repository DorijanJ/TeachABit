using Microsoft.AspNetCore.Identity;
using TeachABit.API.Middleware;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Repository.Repositories.Objave;
using TeachABit.Repository.Repositories.Radionice;
using TeachABit.Repository.Repositories.Tecajevi;
using TeachABit.Service.Services.Authentication;
using TeachABit.Service.Services.Authorization;
using TeachABit.Service.Services.Objave;
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
            services.AddScoped<SignInManager<Korisnik>>();
            services.AddScoped<UserManager<Korisnik>>();
            services.AddScoped<IMailSenderService, MailSenderService>();
            services.AddScoped<ITecajeviRepository, TecajeviRepository>();
            services.AddScoped<ITecajeviService, TecajeviService>();
            services.AddScoped<IObjaveRepository, ObjaveRepository>();
            services.AddScoped<IObjaveService, ObjaveService>();
            services.AddScoped<IRadioniceRepository, RadioniceRepository>();
            services.AddScoped<IRadioniceService, RadioniceService>();
            services.AddScoped<ModelStateFilter>();
            return services;
        }
    }
}
