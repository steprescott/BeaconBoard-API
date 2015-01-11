using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Domain
{
    public class Room
    {
        public Guid RoomID { get; set; }
        public string Number { get; set; }

        public List<Guid> BeaconIDs { get; set; }
    }
}
