using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WhiteboardAPI.Data;
using WhiteboardAPI.Data.Data_Seeds;
using WhiteboardAPI.Resources;
using WhiteboardAPI.Models.Assignments;
using WhiteboardAPI.Models.Accounts;

namespace WhiteboardAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            SeedDatabase(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SeedDatabase(IHost host)
        {
            var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var announcementContext = scope.ServiceProvider.GetRequiredService<AnnouncementContext>();

            if (announcementContext.Database.EnsureCreated()) {
                try {
                    SeedData.Initialize(announcementContext);
                }
                catch (Exception ex) {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "A database seeding error occurred.");
                    ErrorLogger.logError(ex);
                }
            }
            var accountContext = scope.ServiceProvider.GetRequiredService<AccountContext>();
            if (accountContext.Database.EnsureCreated()) {
                try {
                    SeedAccountData.Initialize(accountContext);
				}
                catch (Exception ex) {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "A database seeding error occurred.");
                    ErrorLogger.logError(ex);
                }
            }
            var pollContext = scope.ServiceProvider.GetRequiredService<PollContext>();
            if (pollContext.Database.EnsureCreated()) {
                try {
                    SeedPollData.Initialize(pollContext);
				} catch (Exception e) {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "Poll seeding failed.");
                    ErrorLogger.logError(e);
				}
			}
        }
    }
}
