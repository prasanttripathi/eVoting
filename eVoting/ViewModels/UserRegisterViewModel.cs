using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using GovernmentDBModels;

namespace eVoting.ViewModels
{
    public class UserRegisterViewModel
    {
          [StringLength(10)]
       [Key]
        [Required(ErrorMessage="* Please provide Voter ID")]
        public string VoterId { get; set; }
          [StringLength(10)]
          [Required(ErrorMessage = "* Please provide Marksheet ID")]
        public string MarksheetId { get; set; }
          public string Name { get; set; }
          public string FathersName { get; set; }

          public DateTime DateOfBirth { get; set; }
          public string Marksheettype { get; set; }
          public string Address { get; set; }
          public string District { get; set; }
          public string State { get; set; }
        
    }
}