using AutoMapper;
using BB.Domain.Enums;
using BB.Interfaces;
using BB.UnitOfWorkEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.BusinessLogicEntityFramework.Logic
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserBusinessLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool UserExists(Guid id)
        {
            return _unitOfWork.GetAll<User>().Any(i => i.UserID == id);
        }

        public List<Domain.User> GetAll()
        {
            //Get all the Entity Framework items from the database
            var items = _unitOfWork.GetAll<User>().ToList();

            //Map all the Entity Framework items to a list of domain objects
            return Mapper.Map<List<Domain.User>>(items);
        }

        public Domain.User GetByID(Guid id)
        {
            //Get all the Entity Framework item with the given ID from the database
            var obj = _unitOfWork.GetById<User>(id);

            //Map the Entity Framework item to a domain object
            return Mapper.Map<Domain.User>(obj);
        }

        public Domain.User GetUserForToken(Guid userToken)
        {
            var obj = _unitOfWork.GetAll<User>().SingleOrDefault(i => i.Token == userToken);
            return Mapper.Map<Domain.User>(obj);
        }

        public CRUDResult Delete(Domain.User domainObject)
        {
            //Use the ID of the domain object to call the DeleteByID function
            return DeleteByID(domainObject.UserID);
        }

        public CRUDResult DeleteByID(Guid id)
        {
            try
            {
                //Get back the Entity Framework object from the database with the given ID
                var obj = _unitOfWork.GetById<User>(id);

                //If the object is in the database
                if (obj != null)
                {
                    //Delete it from the database
                    _unitOfWork.Delete(obj);
                    _unitOfWork.SaveChanges();
                    return CRUDResult.Deleted;
                }

                //No object found with the given ID
                return CRUDResult.NotFound;
            }
            catch (Exception)
            {
                //An error has occurred.
                //We don't want to return the over the API as it could
                //expose sensitive information, code snippets and / or stack trace.
                return CRUDResult.Error;
            }
        }
    }
}
