using AutoMapper;
using BB.Domain.Enums;
using BB.Interfaces;
using BB.UnitOfWorkEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.BusinessLogicEntityFramework.Logic
{
    public class ResourceBusinessLogic : IResourceBusinessLogic
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResourceBusinessLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool ResourceExists(Guid id)
        {
            return _unitOfWork.GetAll<Resource>().Any(i => i.ResourceID == id);
        }

        public ResourceResult Create(Domain.Resource domainObject)
        {
            try
            {
                //Check to see if the ID has been set on the domain object already
                if (domainObject.ResourceID == Guid.Empty)
                {
                    //If it hasn't been set generate a new GUID
                    domainObject.ResourceID = Guid.NewGuid();
                }

                //Map the domain object to an Entity Framework object
                var obj = Mapper.Map<Resource>(domainObject);

                //Insert it in the database
                _unitOfWork.Insert(obj);
                _unitOfWork.SaveChanges();
                return ResourceResult.Created;
            }
            catch (Exception exception)
            {
                //An error has occurred.
                //We don't want to return the exception over the API as it could
                //expose sensitive information, code snippets and / or stack trace.
                return ResourceResult.Error;
            }
        }

        public ResourceResult Update(Domain.Resource domainObject)
        {
            //Check first that an ID has been passed
            if (domainObject.ResourceID != Guid.Empty)
            {
                //Query the database to see if we already have an object with the same ID
                var obj = _unitOfWork.GetById<Resource>(domainObject.ResourceID);

                try
                {
                    //If we have the object in the database ready to update
                    if (obj != null)
                    {
                        //Map the updated values
                        obj = Mapper.Map(domainObject, obj);

                        //Update the database to reflect these changes
                        _unitOfWork.Update(obj);
                        _unitOfWork.SaveChanges();
                        return ResourceResult.Updated;
                    }
                    else
                    {
                        //An object with that ID has not been found
                        return ResourceResult.NotFound;
                    }
                }
                catch (Exception exception)
                {
                    //An error has occurred.
                    //We don't want to return the exception over the API as it could
                    //expose sensitive information, code snippets and / or stack trace.
                    return ResourceResult.Error;
                }
            }

            return ResourceResult.Error;
        }

        public List<Domain.Resource> GetAll()
        {
            //Get all the Entity Framework items from the database
            var items = _unitOfWork.GetAll<Resource>().ToList();

            //Map all the Entity Framework items to a list of domain objects
            return Mapper.Map<List<Domain.Resource>>(items);
        }

        public Domain.Resource GetByID(Guid id)
        {
            //Get all the Entity Framework item with the given ID from the database
            var obj = _unitOfWork.GetById<Resource>(id);

            //Map the Entity Framework item to a domain object
            return Mapper.Map<Domain.Resource>(obj);
        }

        public ResourceResult Delete(Domain.Resource domainObject)
        {
            //Use the ID of the domain object to call the DeleteByID function
            return DeleteByID(domainObject.ResourceID);
        }

        public ResourceResult DeleteByID(Guid id)
        {
            try
            {
                //Get back the Entity Framework object from the database with the given ID
                var obj = _unitOfWork.GetById<Resource>(id);

                //If the object is in the database
                if (obj != null)
                {
                    //Delete it from the database
                    _unitOfWork.Delete(obj);
                    _unitOfWork.SaveChanges();
                    return ResourceResult.Deleted;
                }

                //No object found with the given ID
                return ResourceResult.NotFound;
            }
            catch (Exception)
            {
                //An error has occurred.
                //We don't want to return the exception over the API as it could
                //expose sensitive information, code snippets and / or stack trace.
                return ResourceResult.Error;
            }
        }
    }
}
