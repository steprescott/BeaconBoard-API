using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Domain
{
    public class Session
    {
        public Guid SessionID { get; set; }
        public DateTime ScheduledDate { get; set; }
        public Guid LessonID { get; set; }
        public Guid RoomID { get; set; }

        public List<Guid> LecturerIDs { get; set; }
    }
}
