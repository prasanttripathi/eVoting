using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovernmentDBModels
{
   [Table("tblUserVoterIDDetails")]
    public class UserVoterIDDetails
    {
        [Key]
        [StringLength(10)]
        public string VoterId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Father's Name")]
        public string FathersName { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

       [Display(Name = "Date of Birth")]
       [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string District { get; set; }

        [Required]
        [StringLength(20)]
        public string State { get; set; }
    }

}
