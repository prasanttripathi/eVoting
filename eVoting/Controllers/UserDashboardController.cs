using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using eVoting.Common;
using eVoting.UserLoginModel;
using eVoting.BusinessLayer;
using eVoting.OtherDBContextModel;
using eVoting.Models;
using eVoting.ViewModels;
using eVoting.OtherDBContextModel;

namespace eVoting.Controllers
{

    public class UserDashboardController : Controller
    {
        // GET: UserDashboard

        [MyAuthorize(Roles = "BasicUser,SuperAdmin,MainAdmin")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [MyAuthorize(Roles = "BasicUser")]
        public ActionResult DonateYourIdeas()
        {

            if (Session["UserID"] == null)
            {
                RedirectToAction("Login", "Account");
            }
            return View();

        }

        [MyAuthorize(Roles = "BasicUser")]
        [HttpPost]
        public ActionResult DonateYourIdeas(string IdeaFor, string Details)
        {
            if ((IdeaFor == null || IdeaFor == "") || (Details == null || Details == ""))
            {
                ViewBag.valid = "No";
                return View();
            }
            else
            {
                int userID = Convert.ToInt32(Session["UserID"]);
                UserLoginDBContext userLoginDBContext = new UserLoginDBContext();
                UserLoginDetail obj = userLoginDBContext.UserLoginDetails.SingleOrDefault(x => x.UserID == userID);

                GovernmentDBContext governmentDBContext = new GovernmentDBContext();

                string Submitter = (governmentDBContext.UserVoterIDDetails.FirstOrDefault(x => x.VoterId == obj.VoterId).Name).ToString();
                string SubmitterEmailID = obj.Email;

                Idea idea = new Idea
                {
                    IdeaFor = IdeaFor,
                    Details = Details,
                    IsAcknowledged = false,
                    IsViewed = false,
                    comments = "New",
                    Submitter = Submitter,
                    SubmitterEmailID = SubmitterEmailID
                };

                ViewBag.valid = "Yes";

                EvotingCommonDBContext evotingCommonDBContext = new EvotingCommonDBContext();
                evotingCommonDBContext.Ideas.Add(idea);
                evotingCommonDBContext.SaveChanges();
                return View();
            }



        }

       
        public ActionResult AllRequest()
        {
            functions function = new functions();
            int userID = Convert.ToInt32(Session["UserID"]);

            AllUserRequestIdeaAndComplaint allUserRequestIdeaAndComplaint = new AllUserRequestIdeaAndComplaint();
           
            allUserRequestIdeaAndComplaint=function.GetYourAllActivities(userID);
            ViewBag.initial = "Initial";
            return View(allUserRequestIdeaAndComplaint);

        }


         [AllowAnonymous]
        public ActionResult LatestNews()
        {

            return View();

        }

         [AllowAnonymous]
         public ActionResult About()
         {
             return View();

         }

         [AllowAnonymous]
         public ActionResult OurChannels()
         {
             return View();

         }

         [AllowAnonymous]
         public ActionResult Help()
         {
             return View();

         }

         [HttpGet]
         [MyAuthorize(Roles = "BasicUser")]
         public ActionResult Complaints()
         {
             
             return View();

         }

        [HttpPost]
        [MyAuthorize(Roles = "BasicUser")]
         public ActionResult Complaints(string ComplaintFor,string Details)
         {
             if ((ComplaintFor == null || ComplaintFor == "") || (Details == null || Details == ""))
             {
                 ViewBag.valid = "No";
                 return View();
             }
             else
             {
                 int userID = Convert.ToInt32(Session["UserID"]);
                 UserLoginDBContext userLoginDBContext = new UserLoginDBContext();
                 UserLoginDetail obj = userLoginDBContext.UserLoginDetails.SingleOrDefault(x => x.UserID == userID);

                 GovernmentDBContext governmentDBContext = new GovernmentDBContext();

                 string Submitter = (governmentDBContext.UserVoterIDDetails.FirstOrDefault(x => x.VoterId == obj.VoterId).Name).ToString();
                 string SubmitterEmailID = obj.Email;


                 UserComplaintModel  complaint = new UserComplaintModel()
                 {                   
                     ComplaintFor = ComplaintFor,
                     Details = Details,
                     IsAcknowledged = false,
                     IsViewed = false,
                     comments = "New",
                     Submitter = Submitter,
                     SubmitterEmail = SubmitterEmailID
                 };

               
                UserComplaints userComplaint = new UserComplaints()
                 {
                     ComplaintFor = complaint.ComplaintFor,
                     Details = complaint.Details,
                     IsAcknowledged = complaint.IsAcknowledged,
                     IsViewed = complaint.IsViewed,
                     comments = complaint.comments,
                     Submitter = complaint.Submitter,
                     SubmitterEmail = complaint.SubmitterEmail
                 };


               UserComplaints DBuserComplaint = new UserComplaints();
               DBuserComplaint.ComplaintFor = userComplaint.ComplaintFor;
               DBuserComplaint.comments = userComplaint.comments;
               DBuserComplaint.Details = userComplaint.Details;
               DBuserComplaint.IsAcknowledged = userComplaint.IsAcknowledged;
               DBuserComplaint.IsViewed = userComplaint.IsViewed;
               DBuserComplaint.Submitter = userComplaint.Submitter;
               DBuserComplaint.SubmitterEmail = userComplaint.SubmitterEmail;


                 ViewBag.valid = "Yes";
              

                 EvotingCommonDBContext evotingCommonDBContext = new EvotingCommonDBContext();
                 evotingCommonDBContext.UserComplaints.Add(DBuserComplaint);
                 evotingCommonDBContext.SaveChanges();
                 return View();
             }



         }


          [HttpGet]
        [MyAuthorize(Roles = "BasicUser")]
        public ActionResult ElectionInProcess()
        {
            
            if (Session["UserID"] != null)
            {
                int userID = Convert.ToInt32(Session["UserID"]);
                UserLoginDBContext userLoginDBContext = new UserLoginDBContext();
                UserLoginDetail obj = userLoginDBContext.UserLoginDetails.SingleOrDefault(x => x.UserID == userID);
              
                GovernmentDBContext governmentDBContext = new GovernmentDBContext();
 
                string District = (governmentDBContext.UserVoterIDDetails.FirstOrDefault(x => x.VoterId == obj.VoterId).District).ToString();
                @ViewBag.District = District;
              EvotingCommonDBContext context = new EvotingCommonDBContext();
             
             IEnumerable<Area> arealist= context.Areas.Where(x=>x.District==District && x.ElectionProcess==true ).ToList();
             TempData["AreaList"] = arealist;
             return View(arealist);
            }
            else
            {
                RedirectToAction("Login", "Account");
            }

            return View();

        }


          [HttpPost]
          public ActionResult GetYourAreaCandidates(int area)
          {
              ViewBag.Status = "NotVoted";
              if (area.ToString() != null || area.ToString()!="")
              { 
                  EvotingCommonDBContext context = new EvotingCommonDBContext();
                  IEnumerable<ElectionCandidate> electionCandidate = context.ElectionCandidates.Where(x => x.AreaId == area && x.IsActive==true).ToList();
                  return View("CandidateForElection", electionCandidate);
              }
              return View();
          }

        
          public ActionResult CreateCountforCandidate(int CandidateNo)
          {
              EvotingCommonDBContext context = new EvotingCommonDBContext();
                  
              if (CandidateNo.ToString() != null || CandidateNo.ToString()!="")
              { 
                 VoteCount votecount= context.VoteCounts.FirstOrDefault(v => v.CandidateNo == CandidateNo);
                 votecount.Count = votecount.Count + 1;

               int success=  context.SaveChanges();            
               if (success > 0)
               {
                   ViewBag.Status = "Voted";
                   ViewBag.youVotedto = context.CandidateMasters.FirstOrDefault(x => x.CandidateId == votecount.CandidateId).CandidateName;
                   return View("CandidateForElection");
               }
                 
              }

              IEnumerable<ElectionCandidate> electionCandidate = context.ElectionCandidates.Where(x => x.AreaId == CandidateNo && x.IsActive == true).ToList();
              return View("CandidateForElection");
             
          }

        public ActionResult TopCandidatesForSlideshow()
          {

              return View();
          }

    }
}