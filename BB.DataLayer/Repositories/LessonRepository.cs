using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.DataLayer
{
    public partial class LessonRepository
    {
        public bool CreateOrUpdate(Domain.Lesson dominObject)
        {
            //Query the database to see if we already have an object with the same ID
            var obj = GetById(dominObject.LessonID);

            try
            {
                //If we don't have an object with the passed ID
                if (obj == null)
                {
                    //Create a new one
                    obj = new Lesson
                    {
                        LessonID = dominObject.LessonID != Guid.Empty ? dominObject.LessonID : Guid.NewGuid(),
                    };

                    //Insert it into the database
                    Insert(obj);
                }
                else
                {
                    //Update the mutable values
                    obj.LessonID = dominObject.LessonID;

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

        public List<Domain.Lesson> GetAllLessons()
        {
            AutoMapper.Mapper.CreateMap<Lesson, Domain.Lesson>().ForMember(dest => dest.SessionIDs, opt => opt.MapFrom(so => so.Sessions.Select(i => i.SessionID).ToList()))
                                                                .ForMember(dest => dest.ResourceIDs, opt => opt.MapFrom(so => so.Resources.Select(i => i.ResourceID).ToList()))
                                                                .ForMember(dest => dest.CourseIDs, opt => opt.MapFrom(so => so.Courses.Select(i => i.CourseID).ToList()));
            return AutoMapper.Mapper.Map<List<Domain.Lesson>>(GetAll());
        }

        public Domain.Lesson GetLessonByID(Guid id)
        {
            AutoMapper.Mapper.CreateMap<Lesson, Domain.Lesson>().ForMember(dest => dest.SessionIDs, opt => opt.MapFrom(so => so.Sessions.Select(i => i.SessionID).ToList()))
                                                                .ForMember(dest => dest.ResourceIDs, opt => opt.MapFrom(so => so.Resources.Select(i => i.ResourceID).ToList()))
                                                                .ForMember(dest => dest.CourseIDs, opt => opt.MapFrom(so => so.Courses.Select(i => i.CourseID).ToList()));
            return AutoMapper.Mapper.Map<Domain.Lesson>(GetById(id));
        }

        public bool Delete(Domain.Lesson domainObject)
        {
            //Get the object from the database
            var obj = GetById(domainObject.LessonID);

            //If we have a valid object
            if(obj != null)
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
