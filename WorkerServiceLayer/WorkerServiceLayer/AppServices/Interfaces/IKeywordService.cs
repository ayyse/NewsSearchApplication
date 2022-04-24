using System.Collections.Generic;
using WorkerServiceLayer.AppServices.DTOs;

namespace WorkerServiceLayer.AppServices.Interfaces
{
    public interface IKeywordService
    {
        List<KeywordDto> GetAllKeywords();
        void CreateKeyword(KeywordDto item);
        void UpdateKeyword(KeywordDto item);
        void DeleteKeyword(int id);
        KeywordDto GetKeywordById(KeywordDto item);
    }
}
