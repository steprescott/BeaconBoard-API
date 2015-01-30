using BB.Domain;
using BB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Interfaces
{
    public interface IUserBusinessLogic
    {
        bool UserExists(Guid id);

        List<User> GetAll();
        User GetByID(Guid id);
        User GetUserForToken(Guid userToken);

        CRUDResult Delete(User domainObject);
        CRUDResult DeleteByID(Guid id);
    }
}
