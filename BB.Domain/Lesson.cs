using System;
using System.Collections.Generic;
using System.Linq;

namespace BB.Domain
{
    public class Lesson
    {
        public Guid LessonID { get; set; }

        public List<Guid> SessionIDs { get; set; }
        public List<Guid> ResourceIDs { get; set; }
        public List<Guid> CourseIDs { get; set; }
    }
}
