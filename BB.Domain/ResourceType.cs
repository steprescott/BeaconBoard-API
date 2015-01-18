using System;
using System.Collections.Generic;
using System.Linq;

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
