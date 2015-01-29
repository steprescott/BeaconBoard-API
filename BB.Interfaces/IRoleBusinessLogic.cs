using BB.Domain;
using BB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Interfaces
{
    public interface IRoleBusinessLogic
    {
        bool RoleExists(Guid id);

        CRUDResult Create(Role dominObject);

        CRUDResult Update(Role dominObject);

        List<Role> GetAll();
        Role GetByID(Guid id);

        CRUDResult Delete(Role domainObject);
        CRUDResult DeleteByID(Guid id);
    }
}
