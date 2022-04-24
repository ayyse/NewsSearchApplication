using System.Collections.Generic;
using WorkerServiceLayer.AppServices.DTOs;

namespace WorkerServiceLayer.AppServices.Interfaces
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
