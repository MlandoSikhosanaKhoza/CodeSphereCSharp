using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private Dictionary<Type, object> repositories;


        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public GenericRepository<TEntity> GetRepository<TEntity>()
         where TEntity : class
        {
            if (this.repositories == null)
            {
                this.repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);

            if (!this.repositories.ContainsKey(type))
            {
                this.repositories[type] = new GenericRepository<TEntity>(this._context);
            }

            return (GenericRepository<TEntity>)this.repositories[type];

        }
    }
}
