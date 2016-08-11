using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eVoting.ViewModels
{
    public  class UserComplaintModel
    {
     
       [Key]
        public int ComplaintID { get; set; }
        [Display(Name="Complaint for")]
        public string ComplaintFor { get; set; }

        public string Details { get; set; }
        public Nullable<bool> IsViewed { get; set; }
        public Nullable<bool> IsAcknowledged { get; set; }
        public string comments { get; set; }
        public string Submitter { get; set; }
        public string SubmitterEmail { get; set; }
    }
}