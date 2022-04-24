using System;

namespace DataLayer.Entities
{
    public class News : BaseModel
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }
    }
}
