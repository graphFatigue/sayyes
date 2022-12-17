
using sayyes.DAL;
using sayyes.DAL.Interfaces;
using sayyes.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.DAL.Repositories
{
    public class BookRepository : IBaseRepository<Book>
    {

        private readonly ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Book entity)
        {
            await _db.Books.AddAsync(entity);
            await _db.SaveChangesAsync();
        }


        public IQueryable<Book> GetAll()
        {
            return _db.Books;
        }


        public async Task Delete(Book entity)
        {
            _db.Books.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Book> Update(Book entity)
        {
            _db.Books.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}

