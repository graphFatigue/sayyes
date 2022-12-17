
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
    public class BookWritingRepository : IBaseRepository<BookWriting>
    {

        private readonly ApplicationDbContext _db;

        public BookWritingRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(BookWriting entity)
        {
            await _db.BookWriting.AddAsync(entity);
            await _db.SaveChangesAsync();
        }


        public IQueryable<BookWriting> GetAll()
        {
            return _db.BookWriting;
        }


        public async Task Delete(BookWriting entity)
        {
            _db.BookWriting.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<BookWriting> Update(BookWriting entity)
        {
            _db.BookWriting.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}

