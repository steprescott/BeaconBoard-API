using BB.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.UnitOfWorkEntityFramework
{
    public class UnitOfWorkEntityFrameworkImplementation : IUnitOfWork
    {
        private readonly BeaconBoardDatabaseContainer _mscDatabaseContainer;

        public UnitOfWorkEntityFrameworkImplementation()
        {
            _mscDatabaseContainer = new BeaconBoardDatabaseContainer();
            //_mscDatabaseContainer.Database.Log = s => Debug.Write(s);
        }

        public T GetById<T>(object id) where T : class
        {
            return _mscDatabaseContainer.Set<T>().Find(id);
        }

        public T Insert<T>(T entity) where T : class
        {
            return _mscDatabaseContainer.Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _mscDatabaseContainer.Set<T>().Attach(entity);
            _mscDatabaseContainer.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            _mscDatabaseContainer.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _mscDatabaseContainer.Set<T>();
        }

        public IQueryable<T2> GetInheritedSubTypeObjects<T, T2>()
            where T : class
            where T2 : class
        {
            return _mscDatabaseContainer.Set<T>().OfType<T2>();
        }

        public bool HasBeenModified<T>(T entity) where T : class
        {
            return _mscDatabaseContainer.Entry(entity).State == EntityState.Modified;
        }

        public void SaveChanges()
        {
            _mscDatabaseContainer.SaveChanges();
        }

        public void Dispose()
        {
            _mscDatabaseContainer.Dispose();
        }
    }
}
