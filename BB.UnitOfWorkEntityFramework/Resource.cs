//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BB.UnitOfWorkEntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Resource
    {
        public Resource()
        {
            this.Lessons = new HashSet<Lesson>();
        }
    
        public System.Guid ResourceID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URLString { get; set; }
        public System.Guid ResourceTypeID { get; set; }
    
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ResourceType ResourceType { get; set; }
    }
}
