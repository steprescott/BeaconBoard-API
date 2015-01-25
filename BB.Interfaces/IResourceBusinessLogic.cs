using BB.Domain;
using BB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Interfaces
{
    public interface IResourceBusinessLogic
    {
        bool ResourceExists(Guid id);

        CRUDResult Create(Resource dominObject);

        CRUDResult Update(Resource dominObject);

        List<Resource> GetAll();
        Resource GetByID(Guid id);

        CRUDResult Delete(Resource domainObject);
        CRUDResult DeleteByID(Guid id);
    }
}
