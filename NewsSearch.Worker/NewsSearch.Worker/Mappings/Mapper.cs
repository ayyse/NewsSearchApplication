using AutoMapper;
using NewsSearch.Core.Entities;
using NewsSearch.Worker.AppServices.DTOs;

namespace NewsSearch.Worker.Mappings
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
