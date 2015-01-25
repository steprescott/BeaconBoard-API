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

        LessonResult Create(Lesson dominObject);

        LessonResult Update(Lesson dominObject);

        List<Lesson> GetAll();
        Lesson GetByID(Guid id);

        LessonResult Delete(Lesson domainObject);
        LessonResult DeleteByID(Guid id);
    }
}
