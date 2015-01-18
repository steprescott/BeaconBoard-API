using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

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
            return Mapper.Map<List<Domain.Lesson>>(GetAll());
        }

        public Domain.Lesson GetLessonByID(Guid id)
        {
            return Mapper.Map<Domain.Lesson>(GetById(id));
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
