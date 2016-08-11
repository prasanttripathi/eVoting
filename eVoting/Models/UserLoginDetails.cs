using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace eVoting
{
    [Table("tblUserLoginDetails")]
   public class UserLoginDetails
    {            

     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]       
       [Key]
        public int UserID { get; set; }
        [Required]
        [StringLength(15)]

       //[Remote("CheckUserNameExist", "Account", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        public String UserName { get; set; }
        [Required]
        public String Password { get; set; }
        public String Email { get; set; }
        public long MobileNumber { get; set; }

      public  string  VoterId { get; set; }
    }

}
