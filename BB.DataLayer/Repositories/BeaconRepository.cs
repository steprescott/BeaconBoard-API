using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace BB.DataLayer
{
    public partial class BeaconRepository
    {
        public bool BeaconExists(Guid id)
        {
            return GetAll().Any(b => b.BeaconID == id);
        }

        public bool CreateOrUpdate(Domain.Beacon dominObject)
        {
            //Query the database to see if we already have an object with the same ID
            var obj = GetById(dominObject.BeaconID);

            try
            {
                //If we don't have an object with the passed ID
                if(obj == null)
                {
                    //Create a new one
                    obj = new Beacon
                    {
                        BeaconID = dominObject.BeaconID != Guid.Empty ? dominObject.BeaconID : Guid.NewGuid(),
                        Major = dominObject.Major,
                        Minor = dominObject.Minor,
                        RoomID = dominObject.RoomID
                    }; 

                    //Insert it into the database
                    Insert(obj);
                }
                else
                {
                    //Update the mutable values
                    obj.Major = dominObject.Major;
                    obj.Minor = dominObject.Minor;
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

        public List<Domain.Beacon> GetAllBeacons()
        {
            Mapper.CreateMap<Beacon, Domain.Beacon>();
            return Mapper.Map<List<Domain.Beacon>>(GetAll());
        }

        public Domain.Beacon GetBeaconByID(Guid id)
        {
            Mapper.CreateMap<Beacon, Domain.Beacon>();
            return Mapper.Map<Domain.Beacon>(GetById(id));
        }

        public bool Delete(Domain.Beacon domainObject)
        {
            //Get the object from the database
            var obj = GetById(domainObject.BeaconID);

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
