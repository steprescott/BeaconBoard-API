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
    
    public partial class Lesson
    {
        public Lesson()
        {
            this.Courses = new HashSet<Course>();
            this.Sessions = new HashSet<Session>();
            this.Resources = new HashSet<Resource>();
        }
    
        public System.Guid LessonID { get; set; }
    
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
    }
}