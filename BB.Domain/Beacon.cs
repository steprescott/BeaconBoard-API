using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace BB.Domain
{
    /// <summary>
    /// DTO to allow the Entity to be passed over the API.
    /// The App will use the beacons Major and Minor values to identify what room the user is in.
    /// </summary>
    public class Beacon
    {
        /// <summary>
        /// Primary key that is unique for the object.
        /// </summary>
        [Required]
        public Guid BeaconID { get; set; }

        /// <summary>
        /// This value is used to find a beacon. Combined with the Minor value it should be unique.
        /// </summary>
        [Required]
        public int Major { get; set; }

        /// <summary>
        /// This value is used to find a beacon. Combined with the Major value it should be unique.
        /// </summary>
       [Required]
        public int Minor { get; set; }

        /// <summary>
        /// The ID of the room that the beacon is placed in.
        /// </summary>
        public Guid? RoomID { get; set; }
    }
}
