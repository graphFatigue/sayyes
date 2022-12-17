using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.Domain.ViewModels.Author
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public bool Sex { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfDeath { get; set; }
        public string Nationality { get; set; }
        public string Biography { get; set; }
        public string Avatar { get; set; }
    }
}
