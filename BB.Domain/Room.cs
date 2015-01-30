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
    public class Room
    {
        /// <summary>
        /// Primary key that is unique for the object.
        /// </summary>
        [Required]
        public Guid RoomID { get; set; }
        /// <summary>
        /// This should be unique so the user can validate what room they should be in.
        /// If the App can not guarantee what room the user is in, this number will be shown along side the other Rooms the App sees for the user to choose from.
        /// </summary>
        [Required]
        public string Number { get; set; }

        /// <summary>
        /// An array of Beacon IDs that are in this room.
        /// The App uses the beacons to know what room the user is currently in.
        /// </summary>
        public List<Guid> BeaconIDs { get; set; }

        /// <summary>
        /// An array of Sessions IDs that are ran in this room.
        /// </summary>
        public List<Guid> SessionIDs { get; set; }
    }
}
