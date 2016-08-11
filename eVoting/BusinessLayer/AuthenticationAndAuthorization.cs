using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eVoting.UserLoginModel;


namespace eVoting.BusinessLayer
{
    public class AuthenticationAndAuthorization
    {
        public UserLoginDetail IsValidUser(string userName, string password)
        {
            UserLoginDBContext userLoginDBContext = new UserLoginDBContext();
            UserLoginDetail userLoginDetail = new UserLoginDetail();
            //UserType userType = new UserType();
            //bool userValid = userLoginDBContext.UserLoginDetails.Any(user => user.UserName == userName && user.Password == password);

            if (userLoginDBContext.UserLoginDetails.Any(user => user.UserName == userName && user.Password == password))
            {
                userLoginDetail = userLoginDBContext.UserLoginDetails.Single(user => user.UserName == userName && user.Password == password);
                // userType = userLoginDBContext.UserTypes.Single(x => x.UserTypeID == userLoginDetail.UserTypeID);
            }
            else
            {
                userLoginDetail = null;
            }

            //string UserType = userType.Type;

            return userLoginDetail;
        }


        public int GetUserIDFromDatabase(string Username)
        {          
            UserLoginDBContext userLoginDBContext = new UserLoginDBContext();
            UserLoginDetail userDetailsForID = new UserLoginDetail();
            return userLoginDBContext.UserLoginDetails.Single(u => u.UserName == Username).UserID;
             
        }


        public bool CheckIfUserImageIsAvailableInDatabase(int userID)
        {
            UserLoginDBContext userLoginDBContext = new UserLoginDBContext();
            return userLoginDBContext.UserImages.Any(u => u.UserID == userID);

        }

    }
}