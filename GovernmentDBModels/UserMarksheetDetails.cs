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
    [Table("tblUserMarksheetDetails")]
    public class UserMarksheetDetails
    {
        [Key]
        [StringLength(10)]
        [Display(Name="Marksheet Number")]
        public string MarksheetId { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Father's Name")]
        public string FathersName { get; set; }
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Marksheet Type")]
        public string Marksheettype { get; set; }
    }
}
