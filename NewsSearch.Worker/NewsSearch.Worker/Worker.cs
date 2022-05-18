using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsSearch.Worker.AppServices.DTOs;
using NewsSearch.Worker.AppServices.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsSearch.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IServiceProvider _serviceProvider;
        public IConfiguration Configuration { get; }
        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IConfiguration config)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            Configuration = config;
            var apiKey = Configuration["ApiKey"];
            var exceptionsFolderRoot = Configuration["ExceptionsFolderRoot"];
        }

        string month = DateTime.Today.ToString("MMM");
        string day = DateTime.Today.ToString("dd");

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    CreateNews();

                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    await Task.Delay(10000, stoppingToken);
                }
                catch (Exception ex)
                {
                    DirectoryInfo directory = new DirectoryInfo(Configuration["ExceptionsFolderRoot"]);
                    DirectoryInfo monthDirectory = directory.CreateSubdirectory(month);

                    DirectoryInfo dayDirectory = monthDirectory.CreateSubdirectory(day);

                    string path = dayDirectory.ToString();
                    string txtPath = path + "/Exception.txt";
                    FileStream fs = new FileStream(txtPath, FileMode.Append, FileAccess.Write, FileShare.Write);

                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(DateTimeOffset.Now + " " + ex.Message.ToString());
                    sw.Close();
                }
            }
        }

        public void CreateNews()
        {
            var newsApiClient = new NewsApiClient(Configuration["ApiKey"]);

            var articleResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = "Türkiye",
                Language = Languages.TR,
                SortBy = SortBys.PublishedAt,
                From = new DateTime(2022, 5, 17)
            });


            NewsDto dto = new NewsDto();

            var sortedList = articleResponse.Articles.Reverse<Article>();

            foreach (var response in sortedList)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var keywords = scope.ServiceProvider.GetRequiredService<IKeywordService>().GetAllKeywords();

                    foreach (var keyword in keywords)
                    {
                        var lowerResponse = response.Title.ToLower();
                        var lowerKeyword = keyword.Word.ToString().ToLower();

                        if (lowerResponse.Contains(lowerKeyword) == true)
                        {
                            Console.WriteLine("Keyword: " + keyword.Word + "         " + response.Title);

                            dto.KeywordId = keyword.Id;
                            dto.Word = keyword.Word;
                            dto.Author = response.Author;
                            dto.Title = response.Title;
                            dto.Description = response.Description;
                            dto.Url = response.Url;
                            dto.PublishedAt = (DateTime)response.PublishedAt;
                            dto.Content = response.Content;
                        }
                    }

                    scope.ServiceProvider.GetRequiredService<INewsService>().CreateNews(dto);
                }
            }
        }

        //public void GetNewsByKeywords()
        //{
        //    NewsDto dto = new NewsDto();

        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var news = scope.ServiceProvider.GetRequiredService<INewsService>().GetAllNews();

        //        var keywords = scope.ServiceProvider.GetRequiredService<IKeywordService>().GetAllKeywords();

        //        foreach (var response in news)
        //        {
        //            foreach (var keyword in keywords)
        //            {
        //                var lowerResponse = response.Title.ToLower();
        //                var lowerKeyword = keyword.Word.ToString().ToLower();

        //                if (lowerResponse.Contains(lowerKeyword) == true)
        //                {
        //                    Console.WriteLine("Keyword: " + keyword.Word + "         " + response.Title);
        //                    dto.KeywordDto.Id = keyword.Id;
        //                    dto.Word = keyword.Word;
        //                    CreateNews();
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
