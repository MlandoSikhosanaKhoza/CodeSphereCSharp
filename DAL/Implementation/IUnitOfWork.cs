using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork
    {
        GenericRepository<T> GetRepository<T>() where T : class;

        Task CompleteAsync();
    }
}
