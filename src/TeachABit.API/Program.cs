using TeachABit.API.Configurations;
using TeachABit.API.Seed;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddServices(builder.Configuration)
    .AddDatabase(connectionString)
    .AddIdentityServices(builder.Configuration);

builder.Services.RegisterDependencyInjections();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope = app.Services.CreateScope();
await SeedData.SeedRolesAsync(scope.ServiceProvider);

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
