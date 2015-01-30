using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BB.Domain
{
    /// <summary>
    /// In order for the App to know how to handle a Resource it needs to know it's type.
    /// This entity allows a resource to be of a set of types.
    /// </summary>
    public class ResourceType
    {
        /// <summary>
        /// Primary key that is unique for the object.
        /// </summary>
        [Required]
        public Guid ResourceTypeID { get; set; }
        /// <summary>
        /// Name of the ResourceType such as 'Word Document'.
        /// This may be shown to the user.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Description of the ResourceType.
        /// This may be shown to the user.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// An array of Resources that are of this type.
        /// </summary>
        public List<Guid> ResourceIDs { get; set; }
    }
}
