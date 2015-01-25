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

        ResourceResult Create(Resource dominObject);

        ResourceResult Update(Resource dominObject);

        List<Resource> GetAll();
        Resource GetByID(Guid id);

        ResourceResult Delete(Resource domainObject);
        ResourceResult DeleteByID(Guid id);
    }
}
