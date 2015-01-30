using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        T GetById<T>(object id) where T : class;

        T Insert<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        IQueryable<T> GetAll<T>() where T : class;

        IQueryable<T2> GetInheritedSubTypeObjects<T, T2>()
            where T : class
            where T2 : class;

        bool HasBeenModified<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void SaveChanges();
    }
}
