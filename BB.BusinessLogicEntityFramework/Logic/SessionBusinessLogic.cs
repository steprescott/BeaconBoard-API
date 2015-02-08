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

        public CRUDResult Create(Domain.Session domainObject)
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

                //If there are any Lecturers to map
                if(domainObject.LecturerIDs != null)
                {
                    //Due to a Many - Many relationship it is too complex for Automapper to do.
                    var lecturers = _unitOfWork.GetAll<Lecturer>().Where(i => domainObject.LecturerIDs.Contains(i.UserID)).ToList();

                    //If the Session has Lectures linked to it
                    if (lecturers != null && lecturers.Count > 0)
                    {
                        obj.Lecturers = lecturers;
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

        public CRUDResult Update(Domain.Session domainObject)
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

                        //If there are any Lecturers to map
                        if (domainObject.LecturerIDs != null)
                        {
                            //Due to a Many - Many relationship it is too complex for Automapper to do.
                            var lecturers = _unitOfWork.GetAll<Lecturer>().Where(i => domainObject.LecturerIDs.Contains(i.UserID)).ToList();

                            //If the Session has Lectures linked to it
                            if (lecturers != null && lecturers.Count > 0)
                            {
                                obj.Lecturers = lecturers;
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

        public List<Domain.Session> GetAll()
        {
            //Get all the Entity Framework items from the database
            var items = _unitOfWork.GetAll<Session>().ToList();

            //Map all the Entity Framework items to a list of domain objects
            return Mapper.Map<List<Domain.Session>>(items);
        }

        public List<Domain.Session> GetAllUpcomingSessions()
        {
            //Get all the Sessions from the database that have a end date later than now.
            var items = _unitOfWork.GetAll<Session>().Where(i => i.ScheduledEndDate > DateTime.Now).ToList();

            //Map all the Entity Framework items to a list of domain objects
            return Mapper.Map<List<Domain.Session>>(items);
        }

        public List<Domain.Session> GetAllUpcomingSessionsForCourseWithID(Guid id)
        {
            //Get the course from the database with the given ID
            var course = _unitOfWork.GetById<Course>(id);

            if(course == null)
            {
                return null;
            }

            //Get all the Sessions from the database that have a end date later than now.
            var items = course.Modules.SelectMany(i => i.Lessons).SelectMany(i => i.Sessions).Where(i => i.ScheduledEndDate > DateTime.Now);

            //Map all the Entity Framework items to a list of domain objects
            return Mapper.Map<List<Domain.Session>>(items);
        }

        public List<Domain.Session> GetAllUpcomingSessionsForModuleWithID(Guid id)
        {
            //Get the Module from the database with the given ID
            var module = _unitOfWork.GetById<Module>(id);

            if (module == null)
            {
                return null;
            }

            //Get all the Sessions from the database that have a end date later than now.
            var items = module.Lessons.SelectMany(i => i.Sessions).Where(i => i.ScheduledEndDate > DateTime.Now);

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

        public Domain.Session GetCurrentSessionForRoomWithID(Guid id)
        {
            var obj = _unitOfWork.GetAll<Session>().Where(i => i.RoomID == id && i.ScheduledStartDate < DateTime.Now && i.ScheduledEndDate > DateTime.Now).SingleOrDefault();

            //If there is no current Session for the Room
            if(obj == null)
            {
                //Return null
                return null;
            }

            //Return the Session domain object
            return Mapper.Map<Domain.Session>(obj);
        }

        public CRUDResult Delete(Domain.Session domainObject)
        {
            //Use the ID of the domain object to call the DeleteByID function
            return DeleteByID(domainObject.SessionID);
        }

        public CRUDResult DeleteByID(Guid id)
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
