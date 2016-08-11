using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GovernmentDBModels;
using eVoting.ViewModels;
using System.Runtime.InteropServices;
using eVoting;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eVoting.UserLoginModel;
using System.IO;





namespace eVoting.Controllers
{
    [RoutePrefix("Account")]
    [ValidateInput(false)]
    
    public class AccountController : Controller
    {
        // GET: Account

        GovernmentFilesDBContext context = new GovernmentFilesDBContext();
        EvotingDBContext contextEvDB = new EvotingDBContext();
        public ActionResult Dashboard()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View("Login");

        }

        
        public ActionResult Register()
        {
            ViewData["PartialViewDisplay"] = "PartialViewDisabled";//this is written to disable partial view for the first time
            return View("Register");

        }
        [HttpPost]
        public ActionResult Register(UserRegisterViewModel userRegisterViewModel)
        {
            if (ModelState.IsValid)
            {
                
                UserVoterIDDetails  userVoterIDDetails=null;
                UserMarksheetDetails userMarksheetDetails = null;
                try
                {

                     userVoterIDDetails= context.UservoteridDetails.Single(p => p.VoterId == userRegisterViewModel.VoterId);
                    userMarksheetDetails = context.UsermarksheetDetails.Single(p => p.MarksheetId == userRegisterViewModel.MarksheetId);

                    
                    UserRegisterViewModel userRegisterVM = new UserRegisterViewModel();
                    if ((userVoterIDDetails.Name == userMarksheetDetails.Name) &&
                        (userVoterIDDetails.FathersName == userMarksheetDetails.FathersName))
                    {
                        Session["VID"] = userVoterIDDetails.VoterId;
                        userRegisterVM.VoterId = userVoterIDDetails.VoterId;
                        userRegisterVM.Name = userVoterIDDetails.Name;
                        userRegisterVM.FathersName = userVoterIDDetails.FathersName;
                        userRegisterVM.MarksheetId = userMarksheetDetails.MarksheetId;
                        userRegisterVM.DateOfBirth = userVoterIDDetails.DateOfBirth;
                        userRegisterVM.Marksheettype = userMarksheetDetails.Marksheettype;
                        userRegisterVM.Address = userVoterIDDetails.Address;
                        userRegisterVM.District = userVoterIDDetails.District;
                        userRegisterVM.State = userVoterIDDetails.State;

                        ViewData["PartialViewDisplay"] = "PartialViewEnabled";//make this enable
                        TempData["Success"] = "Cheers !!  You are Authorize to make your e-Voting account";

                        return PartialView(userRegisterVM);
                    }
                    else
                    {
                        TempData["Failed"] = "Oppps !!  There is mismatch of data. You are not Authorize to make your e-Voting account,Please raise a request for correction/new data";

                        return View();
                    }

                }
                catch (Exception)
                {
                    if (userVoterIDDetails == null)
                        TempData["Failed"] = "Oppps !!   It seems Voter Id is not valid . You are not Authorize to make your e-Voting account,Please raise a request for correction/new data";
                    else
                    {                      
                        if (userMarksheetDetails == null)
                            TempData["Failed"] = "Oppps !!   It seems Marksheet Number is not valid . You are not Authorize to make your e-Voting account,Please raise a request for correction/new data";       
                    }
                                              
                    return View();
                }
            }
            else
            {

                return View();
            }
           

        }
       
     //   public ActionResult VoterIDValidity(UserRegisterViewModel userRegisterViewModel)
    
        public ActionResult RegisterPart2()
        {
            return View();

        }
        
        [HttpPost]
        public ActionResult RegisterPart2(UserRegister2Details userRegister2Details)
        {
            if (ModelState.IsValid)
            {

                UserLoginDetails userDetails = new UserLoginDetails();
                if (contextEvDB.userDetails.Count(u => u.UserName == userRegister2Details.UserName)==0)
                {
                   // userDetails.UserID = (int)DatabaseGeneratedOption.Identity;
                  
                userDetails.UserName = userRegister2Details.UserName;
                userDetails.Password = userRegister2Details.Password;
                userDetails.Email = userRegister2Details.Email;
                userDetails.MobileNumber = userRegister2Details.MobileNumber;
                //userDetails.VoterId =  userRegister2Details.VoterId;
                userDetails.VoterId = Session["VID"].ToString();
                contextEvDB.userDetails.Add(userDetails);
                   
                contextEvDB.SaveChanges();

                //fetch user ID
                UserLoginDetails userDetailsForID = new UserLoginDetails();
                userDetailsForID = contextEvDB.userDetails.Single(u => u.UserName == userRegister2Details.UserName);
                Session["UID"] = userDetailsForID.UserID.ToString();
                Session["userRegister2Details"] = userRegister2Details;
              //Session["VID"] = null;
                ViewData["UploadPicture"] = "UploadPicture";
                return View("RegisterDetails", userRegister2Details);
                }
            }

            else
            {
  
                return View();
            }
            return View();

        }

        //this codeis  writen for jquery validation but input is not coming ,username is null in parameter
        //because i was putting "string name" instead of "string input"  ,it should be input coz in js it is input
        [AllowAnonymous]
        public string CheckUserName(string input)
        {
            if (contextEvDB.userDetails.Count(u => u.UserName == input) == 0)
            {
                return "Available";
            }

            else
            {
                return "Not Available";
            }
 
        }
          [AllowAnonymous]
        public string IsVoterIDRegistered(string input)
        {
            UserLoginDBContext userLoginDBContext = new UserLoginDBContext();
            if (userLoginDBContext.UserLoginDetails.Any(v => v.VoterId == input))
                return "Not Available";
            else
                return "Available";

        }

      
        [NonAction]
          public byte[] ConvertToBytes(HttpPostedFileBase image)
          {
              byte[] imageBytes = null;
              BinaryReader reader = new BinaryReader(image.InputStream);
              imageBytes = reader.ReadBytes((int)image.ContentLength);
              return imageBytes;
          }
  


        [HttpPost]
          public ActionResult UploadImageToDatabase()
        {
            UserRegister2Details userRegister2Details = new UserRegister2Details();
            userRegister2Details = (UserRegister2Details)Session["userRegister2Details"];
            ViewData["UploadPicture"] = "UploadPicture";
            HttpPostedFileBase file = Request.Files["ImageData"];

            UserLoginDBContext db = new UserLoginDBContext();
            UserImage userImage = new UserImage();
            userImage.UserID =Convert.ToInt32(Session["UID"]);
            userImage.Image = ConvertToBytes(file);
            db.UserImages.Add(userImage);                    
           int success= db.SaveChanges();

           if (success>0)
           {
               Session["UploadPicture"] = "Success";

           }
           else
           {
               Session["UploadPicture"] = "error";
           }


           return View("RegisterDetails", userRegister2Details);
        }


        
        [HttpGet]
        [AllowAnonymous]
 
        public ActionResult GetImageFromDatabase(int userID)
        {
            UserLoginDBContext db = new UserLoginDBContext();
            UserImage userImage = new UserImage();

            if (db.UserImages.Any(u => u.UserID == userID))
            {

                var q = from temp in db.UserImages where temp.UserID == userID select temp.Image;
                byte[] cover = q.First();

                if (cover != null)
                {                   
                    return File(cover, "image/jpg");
                }
                else
                {
                  
                    return null;
                }
            }
            else
            {

                return null;
            }
               
          
        }
        

        //[HttpPost]
        //public JsonResult CheckUserNameExist(string UserName)
        //{

        //    var user = Membership.GetUser(UserName);

        //    return Json(user == null);
        //}

        [NonAction]
        public string GetRoleBy_UserID(string UserId)
        {
            return "";
        }

        [NonAction]
        public string GetUserID_By_UserName(string UserName)
        {
            return "";
        }


        }
    
}