
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
    public class WritingAuthorRepository : IBaseRepository<WritingAuthor>
    {

        private readonly ApplicationDbContext _db;

        public WritingAuthorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(WritingAuthor entity)
        {
            await _db.WritingAuthor.AddAsync(entity);
            await _db.SaveChangesAsync();
        }


        public IQueryable<WritingAuthor> GetAll()
        {
            return _db.WritingAuthor;
        }


        public async Task Delete(WritingAuthor entity)
        {
            _db.WritingAuthor.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<WritingAuthor> Update(WritingAuthor entity)
        {
            _db.WritingAuthor.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}

