using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace BB.DataLayer
{
    public partial class SessionRepository
    {
        public bool CreateOrUpdate(Domain.Session dominObject)
        {
            //Query the database to see if we already have an object with the same ID
            var obj = GetById(dominObject.SessionID);

            try
            {
                //If we don't have an object with the passed ID
                if (obj == null)
                {
                    //Create a new one
                    obj = new Session
                    {
                        SessionID = dominObject.SessionID != Guid.Empty ? dominObject.SessionID : Guid.NewGuid(),
                        ScheduledDate = dominObject.ScheduledDate,
                        LessonID = dominObject.LessonID,
                        RoomID = dominObject.RoomID
                    };

                    //Insert it into the database
                    Insert(obj);
                }
                else
                {
                    //Update the mutable values
                    obj.ScheduledDate = dominObject.ScheduledDate;
                    obj.LessonID = dominObject.LessonID;
                    obj.RoomID = dominObject.RoomID;

                    //Update the database
                    Update(obj);
                }

                //Save the changes to the database
                SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public List<Domain.Session> GetAllSessions()
        {
            Mapper.CreateMap<Session, Domain.Session>().ForMember(dest => dest.LecturerIDs, opt => opt.MapFrom(c => c.Lecturers.Select(i => i.UserID).ToList()));
            return Mapper.Map<List<Domain.Session>>(GetAll());
        }

        public Domain.Session GetSessionByID(Guid id)
        {
            Mapper.CreateMap<Session, Domain.Session>().ForMember(dest => dest.LecturerIDs, opt => opt.MapFrom(c => c.Lecturers.Select(i => i.UserID).ToList()));
            return Mapper.Map<Domain.Session>(GetById(id));
        }

        public bool Delete(Domain.Session domainObject)
        {
            //Get the object from the database
            var obj = GetById(domainObject.SessionID);

            //If we have a valid object
            if (obj != null)
            {
                try
                {
                    //Delete it
                    Delete(obj);

                    //Save the changes to the database
                    SaveChanges();
                    return true;
                }
                catch (Exception exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool DeleteByID(Guid id)
        {
            try
            {
                //Delete the object with the given ID
                Delete(id);

                //Save the changes to the database
                SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
    }
}
