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
    
    public partial class Student : User
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
            this.Attendances = new HashSet<Attendance>();
        }
    
    
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
