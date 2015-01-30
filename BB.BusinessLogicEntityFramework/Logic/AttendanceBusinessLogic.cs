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
    public class AttendanceBusinessLogic : IAttendanceBusinessLogic
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceBusinessLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public CRUDResult Create(Domain.Attendance domainObject)
        {
            try
            {
                //Check to see if the ID has been set on the domain object already
                if (domainObject.AttendanceID == Guid.Empty)
                {
                    //If it hasn't been set generate a new GUID
                    domainObject.AttendanceID = Guid.NewGuid();
                }

                //Map the domain object to an Entity Framework object
                var obj = Mapper.Map<Attendance>(domainObject);

                //Insert it in the database
                _unitOfWork.Insert(obj);
                _unitOfWork.SaveChanges();
                return CRUDResult.Created;
            }
            catch (Exception)
            {
                //An error has occurred.
                //We don't want to return the over the API as it could
                //expose sensitive information, code snippets and / or stack trace.
                return CRUDResult.Error;
            }
        }

        public List<Domain.Attendance> GetAll()
        {
            //Get all the Entity Framework items from the database
            var items = _unitOfWork.GetAll<Attendance>().ToList();

            //Map all the Entity Framework items to a list of domain objects
            return Mapper.Map<List<Domain.Attendance>>(items);
        }

        public Domain.Attendance GetByID(Guid id)
        {
            //Get all the Entity Framework item with the given ID from the database
            var obj = _unitOfWork.GetById<Attendance>(id);

            //Map the Entity Framework item to a domain object
            return Mapper.Map<Domain.Attendance>(obj);
        }
    }
}
