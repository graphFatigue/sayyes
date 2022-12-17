using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.Domain.Entity
{
    public class BookWriting
    {
        [Key]
        public int RelationId { get; set; }
        public int BookId { get; set; }
        public int WritingId { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }

    }
}
