using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace BB.DataLayer.Repositories
{
    /// <summary>
    /// A generic repository that can be used to carry out generic actions on data within the database
    /// </summary>
    /// <typeparam name="TEntity">The class/entityToAdd that we are acting upon</typeparam>
    public class GenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// The connection context we are acting upon
        /// </summary>
        private readonly DbContext _dataEntities;

        /// <summary>
        /// The set of elements that we are acting upon, set according to the TEntity class/entityToAdd sent through
        /// </summary>
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Boolean indicating whether the entityToAdd is soft deletable
        /// </summary>
        private readonly bool _isSoftDeletableEntity;

        /// <summary>
        /// The method information to get the getter method of the IsDeleted entityToAdd
        /// </summary>
        private readonly MethodInfo _isDeletedGetterMethodInfo;

        /// <summary>
        /// The method information to set the setter method of the IsDeleted entityToAdd
        /// </summary>
        private readonly MethodInfo _isDeletedSetterMethodInfo;

        /// <summary>
        /// The default constructor which sets up the communication means to the database and sets up the 
        /// collection of data we will be acting upon according to the TEntity class/entityToAdd
        /// </summary>
        /// <param name="dataEntities">The database contextual/connection means</param>
        /// <param name="softDeleteField">The field that represents the soft deletable field in the database</param>
        public GenericRepository(DbContext dataEntities, string softDeleteField = "IsDeleted")
        {
            //Set up the context in which to get back data from and the set of data
            //which corresponds to the TEntity class sent through
            _dataEntities = dataEntities;
            _dbSet = _dataEntities.Set<TEntity>();

            //Validate the entityToAdd passed through is a valid DbContext entityToAdd
            ValidateEntity();

            //Determine if the entityToAdd is SoftDeletable
            var type = typeof(TEntity);
            var property = type.GetProperty(softDeleteField);
            if (property != null)
            {
                _isSoftDeletableEntity = true;
                _isDeletedGetterMethodInfo = property.GetMethod;
                _isDeletedSetterMethodInfo = property.SetMethod;
            }
        }

        /// <summary>
        /// Function which validates if a class/entityToAdd exists within a given database context
        /// An exception is thrown if it does not as we cannot proceed
        /// </summary>
        private void ValidateEntity()
        {
            try
            {
                //Load the data as a test!
                _dbSet.Load();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Generic function that allows us to get back an element by ID
        /// </summary>
        /// <param name="id">The ID or primary key denotion of the element we want to get back</param>
        /// <returns>The corresponding element or null if not found</returns>
        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// All the elements for a set class/entityToAdd
        /// </summary>
        /// <param name="includeDeleted">Indicates whether the deleted items should be returned or not in the results</param>
        /// <returns>All the elements for a set class/entityToAdd</returns>
        public virtual IQueryable<TEntity> GetAll(bool includeDeleted = false)
        {
            if (_isSoftDeletableEntity && !includeDeleted)
            {
                var entityList = new List<TEntity>();

                foreach (var entity in _dbSet)
                {
                    if (!((bool)_isDeletedGetterMethodInfo.Invoke(entity, null)))
                    {
                        entityList.Add(entity);
                    }
                }
                return entityList.AsQueryable();
            }

            return _dbSet;
        }

        /// <summary>
        /// All the elements for a set class/entityToAdd that has a conditional predicate to be
        /// carried out upon it
        /// </summary>
        /// <param name="predicate">The condition to be carried out on the collection of elements</param>
        /// <param name="includeDeleted">Indicates whether the deleted items should be returned or not in the results</param>
        /// <returns>All the elements according to the condition</returns>
        public virtual IQueryable<TEntity> GetAllWhere(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false)
        {
            //Should take care of the predicate as well as the handling of the IsDeletable method
            return GetAll(includeDeleted).Cast<TEntity>().Where(predicate);
        }

        /// <summary>
        /// Function that allows the insertion of a generic object according to the TEntity
        /// </summary>
        /// <param name="entityToAdd">The element we are inserting to the data context</param>
        public virtual TEntity Insert(TEntity entityToAdd)
        {
            _dbSet.Add(entityToAdd);
            _dataEntities.Entry(entityToAdd).State = EntityState.Added;
            return entityToAdd;
        }

        /// <summary>
        /// Function that allows the updating of an object already in the database
        /// </summary>
        /// <param name="entityToUpdate">The entityToAdd we have changed that we are updating</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _dataEntities.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// Function that allows the deletion of an element from the database
        /// </summary>
        /// <param name="id">The ID of the entityToAdd we are deleting</param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Function that allows the deletion of a element from a database
        /// </summary>
        /// <param name="entityToDelete">The actual entityToAdd we are deletion</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            //Set the flag accordingly if the entityToAdd is SoftDeletable
            if (_isSoftDeletableEntity)
            {
                //Set the object IsDeleted flag to true
                _isDeletedSetterMethodInfo.Invoke(entityToDelete, new object[] { true });
                Update(entityToDelete);
            }
            else
            {
                if (_dataEntities.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
                _dbSet.Remove(entityToDelete);
            }

            _dataEntities.Entry(entityToDelete).State = EntityState.Deleted;
        }

        /// <summary>
        /// A means in which to save the changes we have just carried out on a database context
        /// </summary>
        public void SaveChanges()
        {
            _dataEntities.SaveChanges();
        }
    }
}