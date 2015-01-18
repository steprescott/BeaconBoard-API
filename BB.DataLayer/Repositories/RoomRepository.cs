using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace BB.DataLayer
{
    public partial class RoomRepository
    {
        public bool CreateOrUpdate(Domain.Room dominObject)
        {
            //Query the database to see if we already have an object with the same ID
            var obj = GetById(dominObject.RoomID);

            try
            {
                //If we don't have an object with the passed ID
                if (obj == null)
                {
                    //Create a new one
                    obj = new Room
                    {
                        RoomID = dominObject.RoomID != Guid.Empty ? dominObject.RoomID : Guid.NewGuid(),
                        Number = dominObject.Number
                    };

                    //Insert it into the database
                    Insert(obj);
                }
                else
                {
                    //Update the mutable values
                    obj.Number = dominObject.Number;

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

        public List<Domain.Room> GetAllRooms()
        {
            Mapper.CreateMap<Room, Domain.Room>().ForMember(dest => dest.BeaconIDs, opt => opt.MapFrom(c => c.Beacons.Select(i => i.BeaconID).ToList()));
            return Mapper.Map<List<Domain.Room>>(GetAll());
        }

        public Domain.Room GetRoomByID(Guid id)
        {
            Mapper.CreateMap<Room, Domain.Room>().ForMember(dest => dest.BeaconIDs, opt => opt.MapFrom(c => c.Beacons.Select(i => i.BeaconID).ToList()));
            return Mapper.Map<Domain.Room>(GetById(id));
        }

        public bool Delete(Domain.Room domainObject)
        {
            //Get the object from the database
            var obj = GetById(domainObject.RoomID);

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
