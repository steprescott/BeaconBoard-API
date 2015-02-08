using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace BB.Domain
{
    /// <summary>
    /// DTO to allow the Entity to be passed over the API.
    /// 
    /// </summary>
    public class Module
    {
        /// <summary>
        /// Primary key that is unique for the object.
        /// </summary>
        [Required]
        public Guid ModuleID { get; set; }

        /// <summary>
        /// The name of the Module that can be displayed to the User.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description of the Module. This may be shown to the User.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// This is the Term number that the Module is ran during.
        /// </summary>
        [Required]
        public int TermNumber { get; set; }

        /// <summary>
        /// An array of Course IDs that incorporate this Module.
        /// </summary>
        public List<Guid> CourseIDs { get; set; }

        /// <summary>
        /// An array of Lesson IDs that are ran in this Module.
        /// </summary>
        public List<Guid> LessonIDs { get; set; }
    }
}
