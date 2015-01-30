using BB.Domain;
using BB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Interfaces
{
    public interface ILessonBusinessLogic
    {
        bool LessonExists(Guid id);

        CRUDResult Create(Lesson dominObject);

        CRUDResult Update(Lesson dominObject);

        List<Lesson> GetAll();
        Lesson GetByID(Guid id);

        CRUDResult Delete(Lesson domainObject);
        CRUDResult DeleteByID(Guid id);
    }
}
