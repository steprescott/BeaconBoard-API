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

        BeaconResult Create(Beacon dominObject);

        BeaconResult Update(Beacon dominObject);

        List<Beacon> GetAll();
        Beacon GetByID(Guid id);

        BeaconResult Delete(Beacon domainObject);
        BeaconResult DeleteByID(Guid id);
    }
}
