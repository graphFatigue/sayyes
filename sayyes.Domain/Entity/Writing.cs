using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.Domain.Entity
{
    public class Writing
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string Genre { get; set; }
        public int YearOfPublication { get; set; }
        public string OriginalLanguage { get; set; }
        public string BriefDescription { get; set; }
        public int Pages { get; set; }
    }
}
