using BB.Domain;
using BB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Interfaces
{
    public interface IStudentBusinessLogic
    {
        CRUDResult Create(Student dominObject);

        CRUDResult Update(Student dominObject);

        List<Student> GetAll();
        Student GetByID(Guid id);

        CRUDResult Delete(Student domainObject);
        CRUDResult DeleteByID(Guid id);
    }
}
