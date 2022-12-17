using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.Domain.Entity
{
    public class BookAuthor
    {
        [Key]
        public int RelationId { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }
}
