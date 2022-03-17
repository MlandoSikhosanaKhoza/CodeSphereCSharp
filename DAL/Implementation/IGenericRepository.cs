using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> All();
        IQueryable<TEntity> GetAll();
        TEntity GetById(object Id);
        TEntity Add(TEntity Entity);
        bool Delete(object Id);
        bool Update(TEntity Entity);
    }
}
