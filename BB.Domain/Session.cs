using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BB.Domain
{
    /// <summary>
    /// DTO to allow the Entity to be passed over the API.
    /// A Session is the delivery of a Lesson. This is to avoid redundant / duplicate data.
    /// A session has a date and time it is to be ran, where it is being ran and who by. Using this data we can tell if a User should be there or not.
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Primary key that is unique for the object.
        /// </summary>
        [Required]
        public Guid SessionID { get; set; }
        /// <summary>
        /// The Date and Time that the Session is to start.
        /// This is used by the App to see what Session is currently being taught at the current Date and Time.
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ScheduledStartDate { get; set; }
        /// <summary>
        /// The Date and Time that the Session is to end.
        /// This is used by the App to see what Session is currently being taught at the current Date and Time.
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ScheduledEndDate { get; set; }
        /// <summary>
        /// The ID of the Lesson that is being taught for this Session.
        /// This links the Session to the Resources via the Lesson.
        /// </summary>
        [Required]
        public Guid LessonID { get; set; }
        /// <summary>
        /// The ID of the Room that the Session will be taught in.
        /// This allows the App to know what is the current Session for a given Room.
        /// </summary>
        [Required]
        public Guid RoomID { get; set; }

        /// <summary>
        /// An array of Lecturer IDs that are involved with this Session.
        /// </summary>
        public List<Guid> LecturerIDs { get; set; }
        /// <summary>
        /// An array of Attendance IDs.
        /// This allows the system to see what Students attended this Session.
        /// </summary>
        public List<Guid> AttendanceIDs { get; set; }
    }
}
