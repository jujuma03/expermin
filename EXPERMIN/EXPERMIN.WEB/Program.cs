using EXPERMIN.SERVICE.Storage.Model;
using EXPERMIN.WEB.Services.Portal.Portal.Implementations;
using EXPERMIN.WEB.Services.Portal.Portal.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace EXPERMIN.WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

            #region GENERAL
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            builder.Services.Configure<StorageOptions>(
                builder.Configuration.GetSection("Storage:Upcloud"));


            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            #endregion


            #region SERVICIOS
            builder.Services.AddControllersWithViews();
            builder.Services.AddMemoryCache();
            builder.Services.AddAuthorization();
            // consumir EXPERMIN.API
            builder.Services.AddHttpClient("ExperminApi", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ExperminApi:BaseUrl"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            builder.Services.AddScoped<IPortalService, PortalService>();

            // Compresión de respuestas (HTML, JSON, etc.)
            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            var app = builder.Build();
            #endregion


            #region MIDDLEWARES
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseResponseCompression();
            app.UseAuthorization();
            #endregion

            #region RUTAS
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Portal}/{action=Index}/{id?}");

            app.Run();
            #endregion
        }
    }
}





