using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BB.Domain
{
    /// <summary>
    /// DTO to allow the Entity to be passed over the API.
    /// The Course Entity allows the system to see what Students are enrolled on a course and therefore what Students can attend what Sessions.
    /// It also keeps track of what lessons are taught on the Course and by what Lecturers. 
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Primary key that is unique for the object.
        /// </summary>
        [Required]
        public Guid CourseID { get; set; }
        /// <summary>
        /// The name of the course.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// An array of Lesson IDs that is in the course. 
        /// </summary>
        public List<Guid> LessonIDs { get; set; }
        /// <summary>
        /// An array of Student IDs that are enrolled on the course.
        /// </summary>
        public List<Guid> StudentIDs { get; set; }
        /// <summary>
        /// An array of Lecturer IDs that deliver the course.
        /// </summary>
        public List<Guid> LecturerIDs { get; set; }
    }
}
