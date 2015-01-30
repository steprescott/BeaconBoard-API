using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace BB.Domain
{
    /// <summary>
    /// DTO to allow the Entity to be passed over the API.
    /// A Room can contain several Beacons that can be used by the App to locate what Room the user is in.
    /// From that the App can determine what Session is currently running and validate if the user should be there.
    /// If they are the Room entity can link the App to the Lesson of for the current Session and give them access to the Resources of the Lesson.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Primary key that is unique for the object.
        /// </summary>
        [Required]
        public Guid RoleID { get; set; }
        /// <summary>
        /// This is a value that could be displayed to the user in order to identify a Users Role.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// An array of Users that have this Role.
        /// </summary>
        public List<Guid> UserIDs { get; set; }
    }
}
