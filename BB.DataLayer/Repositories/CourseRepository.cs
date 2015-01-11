using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.DataLayer
{
    public partial class CourseRepository
    {
        public bool CreateOrUpdate(Domain.Course dominObject)
        {
            //Query the database to see if we already have an object with the same ID
            var obj = GetById(dominObject.CourseID);

            try
            {
                //If we don't have an object with the passed ID
                if (obj == null)
                {
                    //Create a new one
                    obj = new Course
                    {
                        CourseID = dominObject.CourseID != Guid.Empty ? dominObject.CourseID : Guid.NewGuid(),
                        Name = dominObject.Name
                    };

                    //Insert it into the database
                    Insert(obj);
                }
                else
                {
                    //Update the mutable values
                    obj.Name = dominObject.Name;

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

        public List<Domain.Course> GetAllCourses()
        {
            AutoMapper.Mapper.CreateMap<Course, Domain.Course>().ForMember(dest => dest.LessonIDs, opt => opt.MapFrom(c => c.Lessons.Select(i => i.LessonID).ToList()))
                                                                .ForMember(dest => dest.StudentIDs, opt => opt.MapFrom(c => c.Students.Select(i => i.UserID).ToList()))
                                                                .ForMember(dest => dest.LecturerIDs, opt => opt.MapFrom(c => c.Lecturers.Select(i => i.UserID).ToList()));
            return AutoMapper.Mapper.Map<List<Domain.Course>>(GetAll());
        }

        public Domain.Lesson GetLessonByID(Guid id)
        {
            AutoMapper.Mapper.CreateMap<Course, Domain.Course>().ForMember(dest => dest.LessonIDs, opt => opt.MapFrom(c => c.Lessons.Select(i => i.LessonID).ToList()))
                                                                .ForMember(dest => dest.StudentIDs, opt => opt.MapFrom(c => c.Students.Select(i => i.UserID).ToList()))
                                                                .ForMember(dest => dest.LecturerIDs, opt => opt.MapFrom(c => c.Lecturers.Select(i => i.UserID).ToList()));
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
}
