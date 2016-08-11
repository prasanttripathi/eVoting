using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using System.Web.Security;
using eVoting.UserLoginModel;

namespace eVoting
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<GovernmentDBModels.GovernmentFilesDBContext>(null);

 
        }

        //Occurs when a security module has established the identity of the user.
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        //let us take out the username now                
                        string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string roles = string.Empty;

                        using (UserLoginDBContext userLoginDBContext = new UserLoginDBContext())
                        {
                            UserLoginDetail userLoginDetail = userLoginDBContext.UserLoginDetails.SingleOrDefault(u => u.UserName == username);
                            UserType userType = new UserType();
                            userType = userLoginDBContext.UserTypes.Single(x => x.UserTypeID == userLoginDetail.UserTypeID);
                            roles = userType.Type;
                        }
                        //let us extract the roles from our own custom cookie


                        //Let us set the Pricipal with our user specific details
                      //  HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(identity,string roles)


                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                          new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
                    }
                    catch (Exception)
                    {
                        //somehting went wrong
                    }
                }
            }
        } 


    }
}
