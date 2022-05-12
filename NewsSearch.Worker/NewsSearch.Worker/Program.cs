using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsSearch.Core;
using NewsSearch.Worker;
using NewsSearch.Worker.AppServices.Interfaces;
using NewsSearch.Worker.AppServices.Services;
using NewsSearch.Worker.Mappings;

namespace NewsSearch.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName = "News Search Service";
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<AppDbContext>(opts =>
                        opts.UseSqlServer(hostContext.Configuration.GetConnectionString("MsSQLConnection")));

                    services.AddAutoMapper(typeof(Mapper));

                    services.AddHostedService<Worker>();

                    services.AddScoped<INewsService, NewsService>();
                    services.AddScoped<IKeywordService, KeywordService>();
                });
    }
}
