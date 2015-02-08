using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BB.Domain
{
    /// <summary>
    /// DTO to allow the Entity to be passed over the API.
    /// A lesson entity is used to reduce the need to input data multiple times. It holds all the data needed for the lesson to be taught but doesn't care by who or when.
    /// If a lesson had a scheduled date then all the meta data such as the resources would needed to be added again breaking normalisation principles.
    /// </summary>
    public class Lesson
    {
        /// <summary>
        /// Primary key that is unique for the object.
        /// </summary>
        [Required]
        public Guid LessonID { get; set; }

        /// <summary>
        /// The name of the Module that can be shown to the User.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// An array of Session IDs that will teach / use this lesson.
        /// </summary>
        public List<Guid> SessionIDs { get; set; }
        /// <summary>
        /// An array of resources used in this Lesson.
        /// </summary>
        public List<Guid> ResourceIDs { get; set; }
        /// <summary>
        /// An array of Modules that this Lesson is linked with.
        /// </summary>
        public List<Guid> ModuleIDs { get; set; }
    }
}
