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

        SessionResult Create(Session dominObject);

        SessionResult Update(Session dominObject);

        List<Session> GetAll();
        Session GetByID(Guid id);

        SessionResult Delete(Session domainObject);
        SessionResult DeleteByID(Guid id);
    }
}
