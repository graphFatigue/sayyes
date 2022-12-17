using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.Domain.Entity
{
    public class BookPublisher
    {
        [Key]
        public int RelationId { get; set; }
        public int BookId { get; set; }
        public int PublisherId { get; set; }
        public string TextOfTheBook { get; set; }
        public int YearOfPublication { get; set; }
        public bool IsOriginalVersion { get; set; }
    }
}
