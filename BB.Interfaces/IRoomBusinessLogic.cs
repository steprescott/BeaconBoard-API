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

        CRUDResult Create(Room dominObject);

        CRUDResult Update(Room dominObject);

        List<Room> GetAll();
        Room GetByID(Guid id);

        CRUDResult Delete(Room domainObject);
        CRUDResult DeleteByID(Guid id);
    }
}
