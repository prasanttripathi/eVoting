using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using eVoting.Common;
using eVoting.UserLoginModel;
using eVoting.OtherDBContextModel;
using eVoting.Models;
using eVoting.ViewModels;


namespace eVoting.BusinessLayer
{
    public  class functions
    {


        //auto generation number in string 

       /* private static string _numbers = "0123456789";
        Random random = new Random();
        private void GenerateNumber()
        {
            StringBuilder builder = new StringBuilder(6);
            string numberAsString = "";
            int numberAsNumber = 0;
            for (var i = 0; i < 6; i++)
            {
                builder.Append(_numbers[random.Next(0, _numbers.Length)]);
            }
            numberAsString = builder.ToString();
            numberAsNumber = int.Parse(numberAsString);
        } */

        //auto generation number in int 

        public  string GenerateRequestNumber()
        {
            Random generator = new Random();

            int digit = generator.Next(100000, 999999);

            return "Req#" + digit;

           
        }
        

        //this is for admins
        public IEnumerable<GeneralRequest> GetAllRequests(int userID)
        {
            //this userID is coming from session
            GovernmentDBContext governmentDBContext = new GovernmentDBContext();
            
            UserLoginDBContext userLoginDBContext = new UserLoginDBContext();
            string voterId = userLoginDBContext.UserLoginDetails.SingleOrDefault(x => x.UserID == userID).VoterId;

    
            string email = governmentDBContext.AllAdminsLists.SingleOrDefault(v => v.VoterID == voterId).Email;
            IEnumerable<GeneralRequest> allRequests = governmentDBContext.GeneralRequests.Where(x => x.Requestedto == email).ToList();

            return allRequests;
        }

        

                //this is for the person who has requested ,users/area admin/main admin can use this

                public IEnumerable<GeneralRequest> GetYourAllRequests(int userID)
                {
                    //this userID is coming from session
                    GovernmentDBContext governmentDBContext = new GovernmentDBContext();

                    UserLoginDBContext userLoginDBContext = new UserLoginDBContext();
                    UserLoginDetail obj=  userLoginDBContext.UserLoginDetails.SingleOrDefault(x => x.UserID == userID);


                    string email = obj.Email;
                    IEnumerable<GeneralRequest> allRequests = governmentDBContext.GeneralRequests.Where(x => x.RequesterEmail == email).ToList();

                    return allRequests;
                } 

        /*
        //for user
        public IEnumerable<Idea> GetYourAllIdeas(int userID)
        {
            //this userID is coming from session
         
            EvotingCommonDBContext evotingCommonDBContext = new EvotingCommonDBContext();

            UserLoginDBContext userLoginDBContext = new UserLoginDBContext();
            UserLoginDetail obj = userLoginDBContext.UserLoginDetails.SingleOrDefault(x => x.UserID == userID);


            string email = obj.Email;
            IEnumerable<Idea> allIdeas = evotingCommonDBContext.Ideas.Where(x => x.SubmitterEmailID == email).ToList();

            return allIdeas;
        } */

      //for user
     /*   public IEnumerable<UserComplaints> GetYourAllComplaint(int userID)
        {
            //this userID is coming from session
               EvotingCommonDBContext evotingCommonDBContext = new EvotingCommonDBContext();
            UserLoginDBContext userLoginDBContext = new UserLoginDBContext();
            UserLoginDetail obj = userLoginDBContext.UserLoginDetails.SingleOrDefault(x => x.UserID == userID);


            string email = obj.Email;
            IEnumerable<UserComplaints> allComplaints = evotingCommonDBContext.UserComplaints.Where(x => x.SubmitterEmail == email).ToList();

            return allComplaints;
        }
        */

        //will give all data
        public AllUserRequestIdeaAndComplaint GetYourAllActivities(int userID)
        {
            
              GovernmentDBContext governmentDBContext = new GovernmentDBContext();
             EvotingCommonDBContext evotingCommonDBContext = new EvotingCommonDBContext();
            UserLoginDBContext userLoginDBContext = new UserLoginDBContext();
             
            //get user email ID
             UserLoginDetail obj=  userLoginDBContext.UserLoginDetails.SingleOrDefault(x => x.UserID == userID);
             string email = obj.Email;

           
            IEnumerable<GeneralRequest> allRequests = governmentDBContext.GeneralRequests.Where(x => x.RequesterEmail == email).ToList();

            IEnumerable<Idea> allIdeas = evotingCommonDBContext.Ideas.Where(x => x.SubmitterEmailID == email).ToList();

            IEnumerable<UserComplaints> allComplaints = evotingCommonDBContext.UserComplaints.Where(x => x.SubmitterEmail == email).ToList();

            AllUserRequestIdeaAndComplaint allUserRequestIdeaAndComplaint = new AllUserRequestIdeaAndComplaint();
            allUserRequestIdeaAndComplaint.AllComplaintsList = allComplaints;
            allUserRequestIdeaAndComplaint.AllRequestList = allRequests;
            allUserRequestIdeaAndComplaint.AllIdeasLists = allIdeas;

            return allUserRequestIdeaAndComplaint;
        }
       

      

    }
}