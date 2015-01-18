using System;
using System.Collections.Generic;
using System.Linq;

namespace BB.Domain
{
    public class Room
    {
        public Guid RoomID { get; set; }
        public string Number { get; set; }

        public List<Guid> BeaconIDs { get; set; }
    }
}
