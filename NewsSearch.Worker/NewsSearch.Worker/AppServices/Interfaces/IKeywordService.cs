using NewsSearch.Worker.AppServices.DTOs;
using System.Collections.Generic;

namespace NewsSearch.Worker.AppServices.Interfaces
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
