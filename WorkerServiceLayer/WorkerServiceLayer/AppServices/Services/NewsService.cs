using System;
using DataLayer;
using AutoMapper;
using System.Linq;
using DataLayer.Entities;
using System.Collections.Generic;
using WorkerServiceLayer.AppServices.DTOs;
using WorkerServiceLayer.AppServices.Interfaces;

namespace WorkerServiceLayer.AppServices.Services
{
    public class NewsService : INewsService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public NewsService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public void CreateNews(NewsDto item)
        {
            var x = GetAllNews().Where(x => x.Url == item.Url).ToList();

            if (x.Count == 0)
            {
                var lastItem = GetAllNews().LastOrDefault();
                if (lastItem.PublishedAt < item.PublishedAt)
                {
                    var createdNew = _mapper.Map<NewsDto, News>(item);
                    _context.News.Add(createdNew);
                    _context.SaveChanges();
                    Console.WriteLine(createdNew.Title);
                    Console.WriteLine("News added");
                }
                else
                {
                    Console.WriteLine("Current news not found");
                }
            }
        }

        public void DeleteNews(int id)
        {
            var deletedNew = _context.News.Where(x => x.Id == id).FirstOrDefault();
            _context.News.Remove(deletedNew);
            _context.SaveChanges();
            Console.WriteLine("News deleted");
        }

        public List<NewsDto> GetAllNews()
        {
            var list = _context.News.ToList();
            var mappingList = _mapper.Map<List<News>, List<NewsDto>>(list);
            return mappingList;
        }

        public NewsDto GetNewsById(NewsDto item)
        {
            var getItem = _context.News.Where(x => x.Id == item.Id).FirstOrDefault();
            var mappingItem = _mapper.Map<News, NewsDto>(getItem);
            return mappingItem;
        }

        public void UpdateNews(NewsDto item)
        {
            var updatedNew = _context.News.Where(x => x.Id == item.Id).FirstOrDefault();
            _context.News.Update(updatedNew);
            _context.SaveChanges();
            Console.WriteLine("News updated");
        }
    }
}
