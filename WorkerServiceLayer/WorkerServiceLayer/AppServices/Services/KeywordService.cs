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
    public class KeywordService : IKeywordService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public KeywordService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void CreateKeyword(KeywordDto item)
        {
            var x = GetAllKeywords().Where(x => x.Word == item.Word).ToList();

            if (x.Count == 0)
            {
                var createdKeyword = _mapper.Map<KeywordDto, Keyword>(item);
                _context.Keywords.Add(createdKeyword);
                _context.SaveChanges();
                Console.WriteLine();
                Console.WriteLine("Keyword added");
            }
            //else
            //{
            //    Console.WriteLine("Keyword exist in database");
            //}
        }

        public void DeleteKeyword(int id)
        {
            var deletedKeyword = _context.Keywords.Where(x => x.Id == id).FirstOrDefault();
            _context.Keywords.Remove(deletedKeyword);
            _context.SaveChanges();
            Console.WriteLine("Keyword deleted");
        }

        public void UpdateKeyword(KeywordDto item)
        {
            var updatedKeyword = _context.Keywords.Where(x => x.Id == item.Id).FirstOrDefault();
            _context.Keywords.Update(updatedKeyword);
            _context.SaveChanges();
            Console.WriteLine("Keyword updated");
        }

        public KeywordDto GetKeywordById(KeywordDto item)
        {
            var getItem = _context.Keywords.Where(x => x.Id == item.Id).FirstOrDefault();
            var mappingItem = _mapper.Map<Keyword, KeywordDto>(getItem);
            return mappingItem;
        }

        public List<KeywordDto> GetAllKeywords()
        {
            var list = _context.Keywords.ToList();
            var mappingList = _mapper.Map<List<Keyword>, List<KeywordDto>>(list);
            return mappingList;
        }
    }
}
