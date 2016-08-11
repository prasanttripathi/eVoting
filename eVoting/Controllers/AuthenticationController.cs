using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eVoting.UserLoginModel;
using eVoting.BusinessLayer;
using System.Web.Security;


namespace eVoting.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string Password, string returnUrl)
        {
            AuthenticationAndAuthorization businessObj = new AuthenticationAndAuthorization();

             UserLoginDetail userLoginDetail= businessObj.IsValidUser(UserName, Password);



            if (userLoginDetail!=null)
            {
               

                 FormsAuthentication.SetAuthCookie(UserName, false);
                 Session["Log"] = "LoggedIn";
                 Session["UserName"] = UserName;


                AuthenticationAndAuthorization obj=new AuthenticationAndAuthorization();
               int UserID= obj.GetUserIDFromDatabase(UserName);               
               Session["UserID"] = UserID;

               if (obj.CheckIfUserImageIsAvailableInDatabase(UserID))
                   Session["HasImage"] = "Yes";
               else
                   Session["HasImage"] = "No";

                switch (userLoginDetail.UserTypeID)
                {
               
                    case  1 :
                        Session["User"]="Users";
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                   && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "UserDashboard");
                        }

                       
                        
                    case 2 :
                        Session["Admin"] = "AreaAdmin";
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                  && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "AreaAdmin");
                        }
                      
                        
                    case 3 :
                        Session["Admin"] = "MainAdmin";
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                  && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "MainAdmin");
                        }
                        

                    case 4 :
                        Session["Admin"] = "Admin";
                         if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                   && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                         {
                             return Redirect(returnUrl);
                         }
                         else
                         {
                             return RedirectToAction("Index", "AdminDashboard");
                         }
                         
                    default:
                         return RedirectToAction("Login", "Authentication");
                                           
                }
            }

               // FormsAuthentication.SetAuthCookie(UserName, false);
               // return RedirectToAction("CentralHome", "GovernmentAdmin");
        
            else
            {
                ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Log"] = "LoggedOut";
            Session["UserName"] = null;
            Session["Admin"] = null;
            Session["User"] = null;
            Session["UID"] = null;
            Session["UploadPicture"] = null;
            Session["Log"] = null;
            Session["UserID"] = null;
            Session["HasImage"] = null;
            Session["userRegister2Details"] = null;

            return RedirectToAction("Index", "Home");
        }

    }


}