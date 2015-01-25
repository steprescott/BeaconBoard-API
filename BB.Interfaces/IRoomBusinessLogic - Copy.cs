using BB.Domain;
using BB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Interfaces
{
    public interface IRoomBusinessLogic
    {
        bool RoomExists(Guid id);

        RoomResult Create(Room dominObject);

        RoomResult Update(Room dominObject);

        List<Room> GetAll();
        Room GetByID(Guid id);

        RoomResult Delete(Room domainObject);
        RoomResult DeleteByID(Guid id);
    }
}
