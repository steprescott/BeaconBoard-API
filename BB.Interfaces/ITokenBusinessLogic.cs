using BB.Domain;
using BB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Interfaces
{
    public interface ITokenBusinessLogic
    {
        TokenResult IsUserTokenValid(Guid userToken);

        Guid? authenticateUsernameAndPassword(String username, String password);  
    }
}
