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
    
    public partial class UserComplaints
    {
        public int ComplaintID { get; set; }
        public string ComplaintFor { get; set; }
        public string Details { get; set; }
        public Nullable<bool> IsViewed { get; set; }
        public Nullable<bool> IsAcknowledged { get; set; }
        public string comments { get; set; }
        public string Submitter { get; set; }
        public string SubmitterEmail { get; set; }
    }
}
