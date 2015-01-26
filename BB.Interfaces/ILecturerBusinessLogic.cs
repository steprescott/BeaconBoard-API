using BB.Domain;
using BB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Interfaces
{
    public interface ILecturerBusinessLogic
    {
        CRUDResult Create(Lecturer dominObject);

        CRUDResult Update(Lecturer dominObject);

        List<Lecturer> GetAll();
        Lecturer GetByID(Guid id);

        CRUDResult Delete(Lecturer domainObject);
        CRUDResult DeleteByID(Guid id);
    }
}
