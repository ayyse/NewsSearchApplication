using DataLayer;
using WorkerServiceLayer.Mappings;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorkerServiceLayer.AppServices.Services;
using Microsoft.Extensions.DependencyInjection;
using WorkerServiceLayer.AppServices.Interfaces;


namespace WorkerServiceLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
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
