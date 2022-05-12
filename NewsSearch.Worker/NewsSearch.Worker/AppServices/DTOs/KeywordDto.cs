using System.Collections.Generic;

namespace NewsSearch.Worker.AppServices.DTOs
{
    public class KeywordDto : BaseDto
    {
        public string Word { get; set; }

        public virtual List<NewsDto> News { get; set; }
    }
}
