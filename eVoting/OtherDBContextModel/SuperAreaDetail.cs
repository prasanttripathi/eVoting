//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eVoting.OtherDBContextModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class SuperAreaDetail
    {
        public int AreaNumber { get; set; }
        public string AreaName { get; set; }
        public string AreaAdminId { get; set; }
        public string ToReportingAdminID { get; set; }
    
        public virtual EvotingAdmin EvotingAdmin { get; set; }
        public virtual EvotingAdmin EvotingAdmin1 { get; set; }
    }
}
