using Stripe;
using TeachABit.API.Configurations;
using TeachABit.API.Middleware;
using TeachABit.API.Seed;
using TeachABit.Service.Util.Stripe;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddServices(builder.Configuration)
    .AddDatabase(connectionString)
    .AddIdentityServices(builder.Configuration);

builder.Services.RegisterDependencyInjections();

var stripeSettings = builder.Configuration.GetSection("Stripe").Get<StripeSettings>();

if (stripeSettings != null)
    StripeConfiguration.ApiKey = stripeSettings.SecretKey;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope = app.Services.CreateScope();
await SeedData.SeedRolesAsync(scope.ServiceProvider);
await SeedData.SeedUser(scope.ServiceProvider);
await SeedData.SeedVerifikacijaStatus(scope.ServiceProvider);
await SeedData.SeedKorisnikStatusi(scope.ServiceProvider);

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseMiddleware<KorisnikStatusCheckMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
