using BB.Domain;
using BB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Interfaces
{
    public interface ISessionBusinessLogic
    {
        bool SessionExists(Guid id);

        CRUDResult Create(Session dominObject);

        CRUDResult Update(Session dominObject);

        List<Session> GetAll();

        List<Session> GetAllUpcomingSessions();

        Session GetByID(Guid id);
        Session GetCurrentSessionForRoomWithID(Guid id);

        CRUDResult Delete(Session domainObject);
        CRUDResult DeleteByID(Guid id);
    }
}
