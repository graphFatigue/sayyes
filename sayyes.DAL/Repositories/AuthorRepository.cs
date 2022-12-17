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
    public class AuthorRepository : IBaseRepository<Author>
    {

        private readonly ApplicationDbContext _db;

        public AuthorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Author entity)
        {
            await _db.Authors.AddAsync(entity);
            await _db.SaveChangesAsync();
        }


        public IQueryable<Author> GetAll()
        {
            return _db.Authors;
        }


        public async Task Delete(Author entity)
        {
            _db.Authors.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Author> Update(Author entity)
        {
            _db.Authors.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}

