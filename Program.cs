using System.Threading.Tasks;
using DeputiTigaKemenpora.Identity;
using Itm.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DeputiTigaKemenpora
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            PermissionGlobalSetting.CustomClaimType = Permissions.CustomClaimTypes;
            PermissionGlobalSetting.SuperPermission = Permissions.All;

            (await BuildWebHostAsync(args)).Run();
            // CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
                .CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging
                        .ClearProviders()
                        .AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task<IHost> BuildWebHostAsync(string[] args)
        {
            IHost host = CreateHostBuilder(args)
                .Build();

            await CheckAddSuperAdminAsync(host);
            return host;
        }

        private static async Task CheckAddSuperAdminAsync(IHost host)
        {
            using IServiceScope scope = host.Services.CreateScope();
            System.IServiceProvider services = scope.ServiceProvider;
            await services.CreateSuperAdmin();
            await services.CreateUserRole();
        }
    }
}