using System.Collections.Generic;

namespace NewsSearch.Core.Entities
{
    public class Keyword : BaseModel
    {
        public string Word { get; set; }

        public virtual List<News> News { get; set; }
    }
}