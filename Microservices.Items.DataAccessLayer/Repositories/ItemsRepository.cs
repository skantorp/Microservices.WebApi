using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microservices.Common.Interfaces;
using Microservices.Items.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Items.DataAccessLayer.Repositories
{
    public class ItemsRepository : IRepository<Item>
    {
        protected readonly DbSet<Item> _dbSet;
        protected ItemContext _db;

        public ItemsRepository(ItemContext db)
        {
            _db = db;
            _dbSet = _db.Set<Item>();
        }
        public async Task<Item> GetOne(Expression<Func<Item, bool>> predicate)
        {
            var entity = await _dbSet
                .Where(predicate)
                .SingleOrDefaultAsync();

            return entity;
        }

        public Task<List<Item>> GetAll()
        {
            return _dbSet
                .ToListAsync();
        }

        public Task Add(Item entity)
        {
            _db.Add(entity);

            return Task.CompletedTask;
        }

        public Task Update(Item entity)
        {
            _dbSet.Update(entity);

            return Task.CompletedTask;
        }

        public Task Remove(Item entity)
        {
            _dbSet.Remove(entity);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<int> SaveChanges()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
