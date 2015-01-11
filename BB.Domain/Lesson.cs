using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
