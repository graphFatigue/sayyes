
using sayyes.DAL.Interfaces;
using sayyes.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.DAL.Repositories
{
    public class WritingRepository : IBaseRepository<Writing>
    {

        private readonly ApplicationDbContext _db;

        public WritingRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Writing entity)
        {
            await _db.Writings.AddAsync(entity);
            await _db.SaveChangesAsync();
        }


        public IQueryable<Writing> GetAll()
        {
            return _db.Writings;
        }


        public async Task Delete(Writing entity)
        {
            _db.Writings.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Writing> Update(Writing entity)
        {
            _db.Writings.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}

