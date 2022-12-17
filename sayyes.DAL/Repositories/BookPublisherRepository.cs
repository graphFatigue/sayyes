
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
    public class BookPublisherRepository : IBaseRepository<BookPublisher>
    {

        private readonly ApplicationDbContext _db;

        public BookPublisherRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(BookPublisher entity)
        {
            await _db.BookPublisher.AddAsync(entity);
            await _db.SaveChangesAsync();
        }


        public IQueryable<BookPublisher> GetAll()
        {
            return _db.BookPublisher;
        }


        public async Task Delete(BookPublisher entity)
        {
            _db.BookPublisher.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<BookPublisher> Update(BookPublisher entity)
        {
            _db.BookPublisher.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}

