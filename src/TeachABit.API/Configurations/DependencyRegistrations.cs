using Amazon.S3;
using Microsoft.AspNetCore.Identity;
using TeachABit.API.Middleware;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Repository.Repositories.Objave;
using TeachABit.Repository.Repositories.Radionice;
using TeachABit.Repository.Repositories.Tecajevi;
using TeachABit.Service.Services.Authentication;
using TeachABit.Service.Services.Authorization;
using TeachABit.Service.Services.Korisnici;
using TeachABit.Service.Services.Objave;
using TeachABit.Service.Services.Radionice;
using TeachABit.Service.Services.Tecajevi;
using TeachABit.Service.Services.Uloge;
using TeachABit.Service.Util.Images;
using TeachABit.Service.Util.Mail;
using TeachABit.Service.Util.S3;
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
            services.AddScoped<IUlogeService, UlogeService>();
            services.AddScoped<Func<string, ModelStateFilter>>(provider => (messageType) =>
            {
                return new ModelStateFilter(messageType);
            });
            services.AddScoped<IKorisniciService, KorisniciService>();
            services.AddAWSService<IAmazonS3>();
            services.AddSingleton<IS3BucketService>(provider =>
            {
                var s3Client = provider.GetRequiredService<IAmazonS3>();
                return new S3BucketService(s3Client, "teachabit");
            });
            services.AddScoped<IImageManipulationService, ImageManipulationService>();
            return services;
        }
    }
}
