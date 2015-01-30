using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Domain
{
    /// <summary>
    /// DTO to allow the Entity to be passed over the API.
    /// Subclass of the User entity, this Entity allows a Lecturer to see what Courses they are related to and what Sessions they are teaching.
    /// </summary>
    public class Lecturer : User
    {
        /// <summary>
        /// An array of Course IDs that the lecture teaches.
        /// </summary>
        public List<Guid> CourseIDs { get; set; }
        /// <summary>
        /// An array of Sessions IDs that the lecture is teaching / involved in.
        /// </summary>
        public List<Guid> SessionIDs { get; set; }
    }
}
