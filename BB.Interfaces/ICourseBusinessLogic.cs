using BB.Domain;
using BB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Interfaces
{
    public interface ICourseBusinessLogic
    {
        bool CourseExists(Guid id);

        CRUDResult Create(Course dominObject);

        CRUDResult Update(Course dominObject);

        List<Course> GetAll();
        Course GetByID(Guid id);

        CRUDResult Delete(Course domainObject);
        CRUDResult DeleteByID(Guid id);
    }
}
