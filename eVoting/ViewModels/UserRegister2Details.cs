using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace eVoting.ViewModels
{
    public class UserRegister2Details
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Please Provide User Name")]
        [RegularExpression(@"([a-zA-Z\d]+[\w\d]*|)[a-zA-Z]+[\w\d.]*", ErrorMessage = "Invalid username")]
        //   [Remote("CheckUserNameExists", "Account", ErrorMessage = "Username already exists!")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Please Provide Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public String Password { get; set; }

        [Required(ErrorMessage = "Please Confirm Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Provide Email ID")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Please Provide MobileNumber")]

        public long MobileNumber { get; set; }
        public string VoterId { get; set; }

    }
}