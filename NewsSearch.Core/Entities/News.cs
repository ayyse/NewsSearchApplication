using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsSearch.Core.Entities
{
    public class News : BaseModel
    {
        public string Word { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }

        public int KeywordId { get; set; }
        public Keyword Keyword { get; set; }
    }
}
