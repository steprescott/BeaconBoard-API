using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Domain
{
    public class Course
    {
        public Guid CourseID { get; set; }
        public string Name { get; set; }

        public List<Guid> LessonIDs { get; set; }
        public List<Guid> StudentIDs { get; set; }
        public List<Guid> LecturerIDs { get; set; }
    }
}
