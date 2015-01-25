using BB.Domain;
using BB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Interfaces
{
    public interface IBeaconBusinessLogic
    {
        bool BeaconExists(Guid id);

        CRUDResult Create(Beacon dominObject);

        CRUDResult Update(Beacon dominObject);

        List<Beacon> GetAll();
        Beacon GetByID(Guid id);

        CRUDResult Delete(Beacon domainObject);
        CRUDResult DeleteByID(Guid id);
    }
}
