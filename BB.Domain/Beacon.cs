using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Domain
{
    public class Beacon
    {
        public Guid BeaconID { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public Guid? RoomID { get; set; }
    }
}
