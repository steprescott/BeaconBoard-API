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
    public class SessionBusinessLogic : ISessionBusinessLogic
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionBusinessLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool SessionExists(Guid id)
        {
            return _unitOfWork.GetAll<Session>().Any(i => i.SessionID == id);
        }

        public SessionResult Create(Domain.Session domainObject)
        {
            try
            {
                //Check to see if the ID has been set on the domain object already
                if (domainObject.SessionID == Guid.Empty)
                {
                    //If it hasn't been set generate a new GUID
                    domainObject.SessionID = Guid.NewGuid();
                }

                //Map the domain object to an Entity Framework object
                var obj = Mapper.Map<Session>(domainObject);

                //Insert it in the database
                _unitOfWork.Insert(obj);
                _unitOfWork.SaveChanges();
                return SessionResult.Created;
            }
            catch (Exception exception)
            {
                //An error has occurred.
                //We don't want to return the exception over the API as it could
                //expose sensitive information, code snippets and / or stack trace.
                return SessionResult.Error;
            }
        }

        public SessionResult Update(Domain.Session domainObject)
        {
            //Check first that an ID has been passed
            if (domainObject.SessionID != Guid.Empty)
            {
                //Query the database to see if we already have an object with the same ID
                var obj = _unitOfWork.GetById<Session>(domainObject.SessionID);

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
                        return SessionResult.Updated;
                    }
                    else
                    {
                        //An object with that ID has not been found
                        return SessionResult.NotFound;
                    }
                }
                catch (Exception exception)
                {
                    //An error has occurred.
                    //We don't want to return the exception over the API as it could
                    //expose sensitive information, code snippets and / or stack trace.
                    return SessionResult.Error;
                }
            }

            return SessionResult.Error;
        }

        public List<Domain.Session> GetAll()
        {
            //Get all the Entity Framework items from the database
            var items = _unitOfWork.GetAll<Session>().ToList();

            //Map all the Entity Framework items to a list of domain objects
            return Mapper.Map<List<Domain.Session>>(items);
        }

        public Domain.Session GetByID(Guid id)
        {
            //Get all the Entity Framework item with the given ID from the database
            var obj = _unitOfWork.GetById<Session>(id);

            //Map the Entity Framework item to a domain object
            return Mapper.Map<Domain.Session>(obj);
        }

        public SessionResult Delete(Domain.Session domainObject)
        {
            //Use the ID of the domain object to call the DeleteByID function
            return DeleteByID(domainObject.SessionID);
        }

        public SessionResult DeleteByID(Guid id)
        {
            try
            {
                //Get back the Entity Framework object from the database with the given ID
                var obj = _unitOfWork.GetById<Session>(id);

                //If the object is in the database
                if (obj != null)
                {
                    //Delete it from the database
                    _unitOfWork.Delete(obj);
                    _unitOfWork.SaveChanges();
                    return SessionResult.Deleted;
                }

                //No object found with the given ID
                return SessionResult.NotFound;
            }
            catch (Exception)
            {
                //An error has occurred.
                //We don't want to return the exception over the API as it could
                //expose sensitive information, code snippets and / or stack trace.
                return SessionResult.Error;
            }
        }
    }
}