using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Domain
{
    public class ResourceType
    {
        public Guid ResourceTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Guid> ResourceIDs { get; set; }
    }
}
