using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity entity);

        TEntity Get(Func<TEntity, bool> predicate);

        IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate);

        TEntity Get(Guid id);

        void Modify(TEntity entity);

        void Remove(TEntity entity);
    }
}
