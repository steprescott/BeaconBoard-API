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
    public class LecturerBusinessLogic : ILecturerBusinessLogic
    {
        private readonly IUnitOfWork _unitOfWork;

        public LecturerBusinessLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CRUDResult Create(Domain.Lecturer domainObject)
        {
            try
            {
                //Check to see if the ID has been set on the domain object already
                if (domainObject.UserID == Guid.Empty)
                {
                    //If it hasn't been set generate a new GUID
                    domainObject.UserID = Guid.NewGuid();
                }

                //Map the domain object to an Entity Framework object
                var obj = Mapper.Map<Lecturer>(domainObject);

                //If there are Courses to map
                if(domainObject.CourseIDs != null)
                {
                    //Due to a Many - Many relationship it is too complex for Automapper to do.
                    var courses = _unitOfWork.GetAll<Course>().Where(i => domainObject.CourseIDs.Contains(i.CourseID)).ToList();

                    //If the Lecturer has Courses linked to it
                    if (courses != null && courses.Count > 0)
                    {
                        obj.Courses = courses;
                    }
                }

                //If there are Sessions to map
                if(domainObject.SessionIDs != null)
                {
                    //Due to a Many - Many relationship it is too complex for Automapper to do.
                    var sessions = _unitOfWork.GetAll<Session>().Where(i => domainObject.SessionIDs.Contains(i.SessionID)).ToList();

                    //If the Lecturer has Sessions linked to it
                    if (sessions != null && sessions.Count > 0)
                    {
                        obj.Sessions = sessions;
                    }
                }

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

        public CRUDResult Update(Domain.Lecturer domainObject)
        {
            //Check first that an ID has been passed
            if (domainObject.UserID != Guid.Empty)
            {
                //Query the database to see if we already have an object with the same ID
                var obj = _unitOfWork.GetById<Lecturer>(domainObject.UserID);

                try
                {
                    //If we have the object in the database ready to update
                    if (obj != null)
                    {
                        //Map the updated values
                        obj = Mapper.Map(domainObject, obj);

                        //If there are Courses to map
                        if (domainObject.CourseIDs != null)
                        {
                            //Due to a Many - Many relationship it is too complex for Automapper to do.
                            var courses = _unitOfWork.GetAll<Course>().Where(i => domainObject.CourseIDs.Contains(i.CourseID)).ToList();

                            //If the Lecturer has Courses linked to it
                            if (courses != null && courses.Count > 0)
                            {
                                obj.Courses = courses;
                            }
                        }

                        //If there are Sessions to map
                        if (domainObject.SessionIDs != null)
                        {
                            //Due to a Many - Many relationship it is too complex for Automapper to do.
                            var sessions = _unitOfWork.GetAll<Session>().Where(i => domainObject.SessionIDs.Contains(i.SessionID)).ToList();

                            //If the Lecturer has Sessions linked to it
                            if (sessions != null && sessions.Count > 0)
                            {
                                obj.Sessions = sessions;
                            }
                        }

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

        public List<Domain.Lecturer> GetAll()
        {
            //Get all the Entity Framework items from the database
            var items = _unitOfWork.GetAll<Lecturer>().ToList();

            //Map all the Entity Framework items to a list of domain objects
            return Mapper.Map<List<Domain.Lecturer>>(items);
        }

        public Domain.Lecturer GetByID(Guid id)
        {
            //Get all the Entity Framework item with the given ID from the database
            var obj = _unitOfWork.GetById<Lecturer>(id);

            //Map the Entity Framework item to a domain object
            return Mapper.Map<Domain.Lecturer>(obj);
        }

        public CRUDResult Delete(Domain.Lecturer domainObject)
        {
            //Use the ID of the domain object to call the DeleteByID function
            return DeleteByID(domainObject.UserID);
        }

        public CRUDResult DeleteByID(Guid id)
        {
            try
            {
                //Get back the Entity Framework object from the database with the given ID
                var obj = _unitOfWork.GetById<Lecturer>(id);

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
