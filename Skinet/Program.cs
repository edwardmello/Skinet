using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.DataContext;
using Microsoft.Extensions.Logging;
using System;
using Infrastructure.DataContext.SeedData;
using Skinet.Helpers;
using Skinet.Middleware;
using Skinet.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SkinetConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddApplicationServices();
builder.Services.AddCors(opt => 
{
    opt.AddPolicy("CorsPolicy", policy =>
     {
         policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:7255");
     });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerDocumentation();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider; ;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try 
    { 
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();  
        await StoreContextSeed.SeedAsync(context,loggerFactory);    
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occured during migrations");
    }
}

app.Run();
