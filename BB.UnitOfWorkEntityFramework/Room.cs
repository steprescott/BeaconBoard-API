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
    
    public partial class Room
    {
        public Room()
        {
            this.Beacons = new HashSet<Beacon>();
            this.Sessions = new HashSet<Session>();
        }
    
        public System.Guid RoomID { get; set; }
        public string Number { get; set; }
    
        public virtual ICollection<Beacon> Beacons { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
