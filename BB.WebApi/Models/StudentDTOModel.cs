using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BB.WebApi.Models
{
    /// <summary>
    /// DTO to allow the Entity to be passed over the API.
    /// Subclass of the User entity, this Entity allows a Student to see what Courses they are enrolled in and therefore what Sessions they can attend.
    /// </summary>
    public class StudentDTOModel : UserDTOModel
    {
        /// <summary>
        /// An array of Course IDs that the student is enrolled in.
        /// </summary>
        public List<Guid> CourseIDs { get; set; }
    }
}