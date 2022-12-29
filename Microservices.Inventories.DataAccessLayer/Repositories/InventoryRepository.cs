using System.Linq.Expressions;
using Microservices.Inventories.DataAccessLayer.Entities;
using Microservices.Inventories.DataAccessLayer.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Inventories.DataAccessLayer.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        protected readonly DbSet<Inventory> _dbSet;
        protected readonly DbSet<InventoryItem> _itemsDbSet;
        protected InventoryContext _db;

        public InventoryRepository(InventoryContext db)
        {
            _db = db;
            _dbSet = _db.Set<Inventory>();
            _itemsDbSet = _db.Set<InventoryItem>();
        }

        public async Task<Inventory> GetOne(Expression<Func<Inventory, bool>> predicate)
        {
            var entity = await _dbSet
                .Include(x => x.Items)
                .Where(predicate)
                .SingleOrDefaultAsync();

            return entity;
        }

        public Task<List<Inventory>> GetAll()
        {
            return _dbSet
                .Include(x => x.Items)
                .ToListAsync();
        }

        public Task Add(Inventory entity)
        {
            _db.Add(entity);

            return Task.CompletedTask;
        }

        public Task Update(Inventory entity)
        {
            _dbSet.Update(entity);

            return Task.CompletedTask;
        }

        public Task Remove(Inventory entity)
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

        public Task RemoveItem(InventoryItem inventoryItem)
        {
            _itemsDbSet.Remove(inventoryItem);

            return Task.CompletedTask;
        }
    }
}
