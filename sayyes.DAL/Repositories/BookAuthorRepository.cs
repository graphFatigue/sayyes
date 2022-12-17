using sayyes.Domain.Entity;
using sayyes.DAL;
using sayyes.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.DAL.Repositories
{
    public class BookAuthorRepository : IBaseRepository<BookAuthor>
    {

        private readonly ApplicationDbContext _db;

        public BookAuthorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(BookAuthor entity)
        {
            await _db.BookAuthor.AddAsync(entity);
            await _db.SaveChangesAsync();
        }


        public IQueryable<BookAuthor> GetAll()
        {
            return _db.BookAuthor;
        }


        public async Task Delete(BookAuthor entity)
        {
            _db.BookAuthor.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<BookAuthor> Update(BookAuthor entity)
        {
            _db.BookAuthor.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}

