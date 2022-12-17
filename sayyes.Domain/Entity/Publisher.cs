using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.Domain.Entity
{
    public class Publisher
    {
        public int Id { get; set; }
        public string NameOfThePublishingHouse { get; set; }
        public int YearOfFoundation { get; set; }
        public string Country { get; set; }
        public string FullNameOfTheDirector { get; set; }
    }
}
