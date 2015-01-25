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
    public class LessonBusinessLogic : ILessonBusinessLogic
    {
        private readonly IUnitOfWork _unitOfWork;

        public LessonBusinessLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool LessonExists(Guid id)
        {
            return _unitOfWork.GetAll<Lesson>().Any(i => i.LessonID == id);
        }

        public LessonResult Create(Domain.Lesson domainObject)
        {
            try
            {
                //Check to see if the ID has been set on the domain object already
                if (domainObject.LessonID == Guid.Empty)
                {
                    //If it hasn't been set generate a new GUID
                    domainObject.LessonID = Guid.NewGuid();
                }

                //Map the domain object to an Entity Framework object
                var obj = Mapper.Map<Lesson>(domainObject);

                //Insert it in the database
                _unitOfWork.Insert(obj);
                _unitOfWork.SaveChanges();
                return LessonResult.Created;
            }
            catch (Exception exception)
            {
                //An error has occurred.
                //We don't want to return the exception over the API as it could
                //expose sensitive information, code snippets and / or stack trace.
                return LessonResult.Error;
            }
        }

        public LessonResult Update(Domain.Lesson domainObject)
        {
            //Check first that an ID has been passed
            if (domainObject.LessonID != Guid.Empty)
            {
                //Query the database to see if we already have an object with the same ID
                var obj = _unitOfWork.GetById<Lesson>(domainObject.LessonID);

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
                        return LessonResult.Updated;
                    }
                    else
                    {
                        //An object with that ID has not been found
                        return LessonResult.NotFound;
                    }
                }
                catch (Exception exception)
                {
                    //An error has occurred.
                    //We don't want to return the exception over the API as it could
                    //expose sensitive information, code snippets and / or stack trace.
                    return LessonResult.Error;
                }
            }

            return LessonResult.Error;
        }

        public List<Domain.Lesson> GetAll()
        {
            //Get all the Entity Framework items from the database
            var items = _unitOfWork.GetAll<Lesson>().ToList();

            //Map all the Entity Framework items to a list of domain objects
            return Mapper.Map<List<Domain.Lesson>>(items);
        }

        public Domain.Lesson GetByID(Guid id)
        {
            //Get all the Entity Framework item with the given ID from the database
            var obj = _unitOfWork.GetById<Lesson>(id);

            //Map the Entity Framework item to a domain object
            return Mapper.Map<Domain.Lesson>(obj);
        }

        public LessonResult Delete(Domain.Lesson domainObject)
        {
            //Use the ID of the domain object to call the DeleteByID function
            return DeleteByID(domainObject.LessonID);
        }

        public LessonResult DeleteByID(Guid id)
        {
            try
            {
                //Get back the Entity Framework object from the database with the given ID
                var obj = _unitOfWork.GetById<Lesson>(id);

                //If the object is in the database
                if (obj != null)
                {
                    //Delete it from the database
                    _unitOfWork.Delete(obj);
                    _unitOfWork.SaveChanges();
                    return LessonResult.Deleted;
                }

                //No object found with the given ID
                return LessonResult.NotFound;
            }
            catch (Exception)
            {
                //An error has occurred.
                //We don't want to return the exception over the API as it could
                //expose sensitive information, code snippets and / or stack trace.
                return LessonResult.Error;
            }
        }
    }
}
