using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Common.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> GetOne(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAll();
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        Task<int> SaveChanges();
    }
}
