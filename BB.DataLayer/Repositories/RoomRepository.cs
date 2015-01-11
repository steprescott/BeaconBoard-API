using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.DataLayer
{
    public partial class RoomRepository
    {
        public bool CreateOrUpdate(Domain.Room dominRoom)
        {
            //Query the database to see if we already have an object with the same ID
            var room = GetById(dominRoom.RoomID);

            try
            {
                //If we don't have an object with the passed ID
                if (room == null)
                {
                    //Create a new one
                    room = new Room
                    {
                        RoomID = dominRoom.RoomID != Guid.Empty ? dominRoom.RoomID : Guid.NewGuid(),
                        Number = dominRoom.Number
                    };

                    //Insert it into the database
                    Insert(room);
                }
                else
                {
                    //Update the mutable values
                    room.Number = dominRoom.Number;

                    //Update the database
                    Update(room);
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
            AutoMapper.Mapper.CreateMap<Room, Domain.Room>().ForMember(dest => dest.BeaconIDs, opt => opt.MapFrom(so => so.Beacons.Select(b => b.BeaconID).ToList()));
            return AutoMapper.Mapper.Map<List<Domain.Room>>(GetAll());
        }

        public Domain.Room GetRoomByID(Guid id)
        {
            AutoMapper.Mapper.CreateMap<Room, Domain.Room>().ForMember(dest => dest.BeaconIDs, opt => opt.MapFrom(so => so.Beacons.Select(b => b.BeaconID).ToList()));
            return AutoMapper.Mapper.Map<Domain.Room>(GetById(id));
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
