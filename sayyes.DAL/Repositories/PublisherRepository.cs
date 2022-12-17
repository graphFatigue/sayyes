
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
    public class PublisherRepository : IBaseRepository<Publisher>
    {

        private readonly ApplicationDbContext _db;

        public PublisherRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Publisher entity)
        {
            await _db.Publishers.AddAsync(entity);
            await _db.SaveChangesAsync();
        }


        public IQueryable<Publisher> GetAll()
        {
            return _db.Publishers;
        }


        public async Task Delete(Publisher entity)
        {
            _db.Publishers.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Publisher> Update(Publisher entity)
        {
            _db.Publishers.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}

