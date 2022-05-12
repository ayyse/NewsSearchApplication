using NewsSearch.Worker.AppServices.DTOs;
using System.Collections.Generic;

namespace NewsSearch.Worker.AppServices.Interfaces
{
    internal interface INewsService
    {
        List<NewsDto> GetAllNews();
        NewsDto GetNewsById(NewsDto item);
        void CreateNews(NewsDto item);
        void UpdateNews(NewsDto item);
        void DeleteNews(int id);
    }
}
