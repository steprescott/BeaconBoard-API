﻿using AutoMapper;
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
    public class ResourceTypeBusinessLogic : IResourceTypeBusinessLogic
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResourceTypeBusinessLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool ResourceTypeExists(Guid id)
        {
            return _unitOfWork.GetAll<ResourceType>().Any(i => i.ResourceTypeID == id);
        }

        public CRUDResult Create(Domain.ResourceType domainObject)
        {
            try
            {
                //Check to see if the ID has been set on the domain object already
                if (domainObject.ResourceTypeID == Guid.Empty)
                {
                    //If it hasn't been set generate a new GUID
                    domainObject.ResourceTypeID = Guid.NewGuid();
                }

                //Map the domain object to an Entity Framework object
                var obj = Mapper.Map<ResourceType>(domainObject);

                //Insert it in the database
                _unitOfWork.Insert(obj);
                _unitOfWork.SaveChanges();
                return CRUDResult.Created;
            }
            catch (Exception)
            {
                //An error has occurred.
                //We don't want to return the over the API as it could
                //expose sensitive information, code snippets and / or stack trace.
                return CRUDResult.Error;
            }
        }

        public CRUDResult Update(Domain.ResourceType domainObject)
        {
            //Check first that an ID has been passed
            if (domainObject.ResourceTypeID != Guid.Empty)
            {
                //Query the database to see if we already have an object with the same ID
                var obj = _unitOfWork.GetById<ResourceType>(domainObject.ResourceTypeID);

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
                        return CRUDResult.Updated;
                    }
                    else
                    {
                        //An object with that ID has not been found
                        return CRUDResult.NotFound;
                    }
                }
                catch (Exception)
                {
                    //An error has occurred.
                    //We don't want to return the over the API as it could
                    //expose sensitive information, code snippets and / or stack trace.
                    return CRUDResult.Error;
                }
            }

            return CRUDResult.Error;
        }

        public List<Domain.ResourceType> GetAll()
        {
            //Get all the Entity Framework items from the database
            var items = _unitOfWork.GetAll<ResourceType>().ToList();

            //Map all the Entity Framework items to a list of domain objects
            return Mapper.Map<List<Domain.ResourceType>>(items);
        }

        public Domain.ResourceType GetByID(Guid id)
        {
            //Get all the Entity Framework item with the given ID from the database
            var obj = _unitOfWork.GetById<ResourceType>(id);

            //Map the Entity Framework item to a domain object
            return Mapper.Map<Domain.ResourceType>(obj);
        }

        public CRUDResult Delete(Domain.ResourceType domainObject)
        {
            //Use the ID of the domain object to call the DeleteByID function
            return DeleteByID(domainObject.ResourceTypeID);
        }

        public CRUDResult DeleteByID(Guid id)
        {
            try
            {
                //Get back the Entity Framework object from the database with the given ID
                var obj = _unitOfWork.GetById<ResourceType>(id);

                //If the object is in the database
                if (obj != null)
                {
                    //Delete it from the database
                    _unitOfWork.Delete(obj);
                    _unitOfWork.SaveChanges();
                    return CRUDResult.Deleted;
                }

                //No object found with the given ID
                return CRUDResult.NotFound;
            }
            catch (Exception)
            {
                //An error has occurred.
                //We don't want to return the over the API as it could
                //expose sensitive information, code snippets and / or stack trace.
                return CRUDResult.Error;
            }
        }
    }
}
