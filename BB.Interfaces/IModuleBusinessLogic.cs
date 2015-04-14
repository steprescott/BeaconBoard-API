using BB.Domain;
using BB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Interfaces
{
    public interface IModuleBusinessLogic
    {
        bool ModuleExists(Guid id);

        CRUDResult Create(Module dominObject);

        CRUDResult Update(Module dominObject);

        List<Module> GetAll();
        Module GetByID(Guid id);

        CRUDResult Delete(Module domainObject);
        CRUDResult DeleteByID(Guid id);
    }
}
