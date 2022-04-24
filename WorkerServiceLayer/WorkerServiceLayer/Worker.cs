using System;
using NewsAPI;
using System.Linq;
using NewsAPI.Models;
using System.Threading;
using NewsAPI.Constants;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorkerServiceLayer.AppServices.DTOs;
using Microsoft.Extensions.DependencyInjection;
using WorkerServiceLayer.AppServices.Interfaces;

namespace WorkerServiceLayer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IServiceProvider _serviceProvider;
        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                CreateNews();
                GetNewsByKeywords();

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }

        public void CreateNews()
        {
            var newsApiClient = new NewsApiClient("fa7766431fda403baddf546cfb2e8ebb");

            var articleResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = "Türkiye",
                Language = Languages.TR,
                SortBy = SortBys.PublishedAt,
                From = new DateTime(2022, 4, 17)
            });


            NewsDto dto = new NewsDto();
            var sortedList = articleResponse.Articles.Reverse<Article>();

            foreach (var response in sortedList)
            {
                dto.Author = response.Author;
                dto.Title = response.Title;
                dto.Description = response.Description;
                dto.Url = response.Url;
                dto.PublishedAt = (DateTime)response.PublishedAt;
                dto.Content = response.Content;


                using (var scope = _serviceProvider.CreateScope())
                {
                    scope.ServiceProvider.GetRequiredService<INewsService>().CreateNews(dto);
                }
            }
        }

        public void GetNewsByKeywords()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var news = scope.ServiceProvider.GetRequiredService<INewsService>().GetAllNews();

                var keywords = scope.ServiceProvider.GetRequiredService<IKeywordService>().GetAllKeywords();

                foreach (var response in news)
                {
                    foreach (var keyword in keywords)
                    {
                        var lowerResponse = response.Title.ToLower();
                        var lowerKeyword = keyword.Word.ToString().ToLower();

                        if (lowerResponse.Contains(lowerKeyword) == true)
                        {
                            Console.WriteLine("Keyword: " + keyword.Word + "         " + response.Title);
                        }
                    }
                }
            }
        }
    }
}
