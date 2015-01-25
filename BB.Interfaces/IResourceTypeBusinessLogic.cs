using BB.Domain;
using BB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Interfaces
{
    public interface IResourceTypeBusinessLogic
    {
        bool ResourceTypeExists(Guid id);

        ResourceTypeResult Create(ResourceType dominObject);

        ResourceTypeResult Update(ResourceType dominObject);

        List<ResourceType> GetAll();
        ResourceType GetByID(Guid id);

        ResourceTypeResult Delete(ResourceType domainObject);
        ResourceTypeResult DeleteByID(Guid id);
    }
}
