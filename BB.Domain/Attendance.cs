using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace BB.Domain
{
    /// <summary>
    /// DTO to allow the Entity to be passed over the API.
    /// </summary>
    public class Attendance
    {
        /// <summary>
        /// Primary key that is unique for the object.
        /// </summary>
        [Required]
        public Guid AttendanceID { get; set; }
        /// <summary>
        /// The SessionID that was attended.
        /// </summary>
        [Required]
        public Guid SessionID { get; set; }
        /// <summary>
        /// The StudentID that attended the Session.
        /// </summary>
        [Required]
        public Guid StudentID { get; set; }
    }
}
