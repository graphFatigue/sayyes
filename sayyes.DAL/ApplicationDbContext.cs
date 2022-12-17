using sayyes.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace sayyes.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Writing> Writings { get; set; }

        public DbSet<BookWriting> BookWriting { get; set; }

        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<BookPublisher> BookPublisher { get; set; }

        public DbSet<WritingAuthor> WritingAuthor { get; set; }


    }
}