using Application;
using Identity;
using Identity.Context;
using Identity.Models;
using Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persitence;
using Persitence.Contexts;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAplicationLayer();
builder.Services.AddIdentityInfraEstructure(builder.Configuration);
builder.Services.AddPersitenceInfraestructure(builder.Configuration);
//builder.Services.AddApiVesioningExtension();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configuracion de roles JWT
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
	try
	{
        var ApplicationContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        ApplicationContext.Database.Migrate();
        var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
        identityContext.Database.Migrate();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultAdminUser.SeedAsync(userManager, roleManager);
        await DefaultBasicUser.SeedAsync(userManager, roleManager);
    }
	catch (Exception)
	{

		throw;
	}
	
    
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.useErrorHandlingMiddleware();

app.MapControllers();

app.Run();
