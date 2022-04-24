using AutoMapper;
using DataLayer.Entities;
using WorkerServiceLayer.AppServices.DTOs;

namespace WorkerServiceLayer.Mappings
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<KeywordDto, Keyword>().ReverseMap();
            CreateMap<NewsDto, News>().ReverseMap();
        }
    }
}
