using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Domain
{
    public class Resource
    {
        public Guid ResourceID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URLString { get; set; }
        public Guid ResourceTypeID { get; set; }

        public List<Guid> LessonIDs { get; set; }
    }
}
