using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BB.Domain
{
    /// <summary>
    /// DTO to allow the Entity to be passed over the API.
    /// A Resource is a file that is used in a Lesson. These normally might be the lecture slides or lab notes.
    /// </summary>
    public class Resource
    {
        /// <summary>
        /// Primary key that is unique for the object.
        /// </summary>
        [Required]
        public Guid ResourceID { get; set; }
        /// <summary>
        /// Name of the Resource that will be shown to the user.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Description of the Resource that will be shown to the user.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// URL to the Resource where it can be accessed.
        /// </summary>
        [Required]
        public string URLString { get; set; }
        /// <summary>
        /// A Resource can be of a set ResourceType. This is used to identify the type of the Resource so the App knows how to open / handle it.
        /// </summary>
        [Required]
        public Guid ResourceTypeID { get; set; }

        /// <summary>
        /// An array of Lessons IDs that use this Resource.
        /// A Resource can be used in several Lessons because their might be a common Resource used across several Lessons.
        /// </summary>
        public List<Guid> LessonIDs { get; set; }
    }
}
