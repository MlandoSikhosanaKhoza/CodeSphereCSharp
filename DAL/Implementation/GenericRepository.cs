using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext Context;
        protected DbSet<TEntity> DataSet;

        public GenericRepository(ApplicationDbContext Context)
        {
            this.Context=Context;
            this.DataSet = Context.Set<TEntity>();
        }
        public TEntity Add(TEntity Entity)
        {
            var addedEntity =  DataSet.Add(Entity);
            Context.SaveChanges();
            return addedEntity.Entity;
        }

        public IEnumerable<TEntity> All()
        {
            return this.DataSet.ToList();
        }

        public IQueryable<TEntity> GetAll()
        {
            return this.DataSet;
        }

        public bool Delete(object Id)
        {
            this.DataSet.Remove(GetById(Id));
            Context.SaveChanges();
            return true;
        }

        public TEntity GetById(object Id)
        {
            return this.DataSet.Find(Id);
        }

        public bool Update(TEntity Entity)
        {
            Context.Entry(Entity).State = EntityState.Modified;
            Context.SaveChanges();
            return true;
        }
    }
}
