using System;
using System.IO;
using DeputiTigaKemenpora.Data;
using Itm.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DeputiTigaKemenpora
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         ConfigureDatabase(services);
         ConfigureSwagger(services);
         ConfigureCookie(services);
         ConfigureMisc(services);

         services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<IdentityDbContext>();
         services.AddRazorPages();
         services.AddMvc();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
         }

         env.CreateUploadFolders();

         app
            .UseHttpsRedirection()
            .UseRouting()
            .UseForwardedHeaders(new ForwardedHeadersOptions
            {
               ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            })
            .UseAuthentication()
            .UseAuthorization();

         string basePath = string.Empty;

         ConfigureEndpoints(app);
         ConfigureStaticFiles(app, env);
         ConfigureSwaggerUI(app, basePath);
         ReadPdfConfiguration();

         PagerUrlHelper.ItemPerPage = 20;
         Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
             Configuration.GetValue<string>("SfLicense"));
      }

      private void ConfigureDatabase(IServiceCollection services)
      {
         services
            .AddDbContextPool<ApplicationDbContext>(options =>
               options.UseMySql(
                  Configuration.GetConnectionString("DefaultConnection"),
                  sqlOptions =>
                  {
                     sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                  }),
               32)
            .AddDbContextPool<IdentityDbContext>(options =>
               options.UseMySql(
                  Configuration.GetConnectionString("IdentityConnection"),
                  sqlOptions =>
                  {
                     sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                  }),
               16);
      }
      private void ConfigureSwagger(IServiceCollection services)
      {
         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kegiatan Deputi 3 API", Version = "v1" });
         });
      }
      private void ConfigureCookie(IServiceCollection services)
      {
         services.ConfigureApplicationCookie(options =>
         {
            options.LoginPath = $"/Identity/Account/Login";
            options.LogoutPath = $"/Identity/Account/Logout";
            options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
         });
      }
      private void ConfigureMisc(IServiceCollection services)
      {
         services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
         services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
         services.AddHttpClient(Microsoft.Extensions.Options.Options.DefaultName);
      }
      private void ConfigureEndpoints(IApplicationBuilder app)
      {
         app.UseEndpoints(endpoints =>
         {
            endpoints.MapRazorPages();
            endpoints.MapControllers();
         });
      }
      private void ConfigureStaticFiles(IApplicationBuilder app, IWebHostEnvironment env)
      {
         app
            .UseStaticFiles()
            .UseStaticFiles(new StaticFileOptions
            {
               FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "upload")),
               RequestPath = new PathString("/upload")
            });
      }
      private void ConfigureSwaggerUI(IApplicationBuilder app, string basePath)
      {
         app
            .UseSwagger()
            .UseSwaggerUI(c =>
            {
               c.SwaggerEndpoint($"{basePath}/swagger/v1/swagger.json", "Kegiatan Deputi 3 API V1");
            });
      }
      private void ReadPdfConfiguration()
      {
         IConfigurationSection generatePdfSection = Configuration.GetSection("GeneratePdf");

         GeneratePdfHelper.WebKitPath = generatePdfSection
            .GetSection("WebKitPath")
            .Value;
         GeneratePdfHelper.DefaultOutputFileName = generatePdfSection
            .GetSection("DefaultFileName")
            .Value;
         GeneratePdfHelper.MarginTopMm = GeneratePdfHelper.ReadMarginFromConfig(
            generatePdfSection,
            "MarginTopMm");
         GeneratePdfHelper.MarginBottomMm = GeneratePdfHelper.ReadMarginFromConfig(
            generatePdfSection,
            "MarginBottomMm");
         GeneratePdfHelper.MarginLeftMm = GeneratePdfHelper.ReadMarginFromConfig(
            generatePdfSection,
            "MarginLeftMm");
         GeneratePdfHelper.MarginRightMm = GeneratePdfHelper.ReadMarginFromConfig(
            generatePdfSection,
            "MarginRightMm");
      }
   }
}
