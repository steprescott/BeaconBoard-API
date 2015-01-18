using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace BB.DataLayer
{
    public partial class ResourceRepository
    {
        public bool CreateOrUpdate(Domain.Resource dominObject)
        {
            //Query the database to see if we already have an object with the same ID
            var obj = GetById(dominObject.ResourceID);

            try
            {
                //If we don't have an object with the passed ID
                if (obj == null)
                {
                    //Create a new one
                    obj = new Resource
                    {
                        ResourceID = dominObject.ResourceID != Guid.Empty ? dominObject.ResourceID : Guid.NewGuid(),
                        Name = dominObject.Name,
                        Description = dominObject.Description,
                        URLString = dominObject.URLString,
                        ResourceTypeID = dominObject.ResourceTypeID
                    };

                    //Insert it into the database
                    Insert(obj);
                }
                else
                {
                    //Update the mutable values
                    obj.Name = dominObject.Name;
                    obj.Description = dominObject.Description;
                    obj.URLString = dominObject.URLString;
                    obj.ResourceTypeID = dominObject.ResourceTypeID;

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

        public List<Domain.Resource> GetAllResources()
        {
            Mapper.CreateMap<Resource, Domain.Resource>().ForMember(dest => dest.LessonIDs, opt => opt.MapFrom(c => c.Lessons.Select(i => i.LessonID).ToList()));
            return Mapper.Map<List<Domain.Resource>>(GetAll());
        }

        public Domain.Resource GetRecourceByID(Guid id)
        {
            Mapper.CreateMap<Resource, Domain.Resource>().ForMember(dest => dest.LessonIDs, opt => opt.MapFrom(c => c.Lessons.Select(i => i.LessonID).ToList()));
            return Mapper.Map<Domain.Resource>(GetById(id));
        }

        public bool Delete(Domain.Resource domainObject)
        {
            //Get the object from the database
            var obj = GetById(domainObject.ResourceID);

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
