using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace eVoting.Models
{
    public class ElectionPartyModel
    {            
        
            public int PartyId { get; set; }
        [Display(Name = "Party Name")]
        [Required]
            public string PartyName { get; set; }
         [Display(Name = "Party Head")]
            public string PartyHead { get; set; }
          [Display(Name = "Party HeadOffice")]
            public string PartyHeadOffice { get; set; }
            public byte[] PartyLogo { get; set; }
         [Display(Name = "is party is currently active?")]
            public Nullable<bool> IsActive { get; set; }
        
    }
}