using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eVoting.UserLoginModel;
using eVoting.Common;
using eVoting.BusinessLayer;
using eVoting.OtherDBContextModel;
using System.IO;
using eVoting.Models;

namespace eVoting.Controllers
{
     [MyAuthorize(Roles = "MainAdmin,SuperAdmin")]
    public class MainAdminController : Controller
    {
        GovernmentDBContext governmentDBContext = new GovernmentDBContext();
        // GET: MainAdmin


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllRequest()
        {
            functions function = new functions();
            int userID = Convert.ToInt32(Session["UserID"]);

            IEnumerable<GeneralRequest> allRequests = function.GetAllRequests(userID);
            return View(allRequests);

        }


        public ActionResult GetYourRequestStatus()
        {
            functions function = new functions();
            int userID = Convert.ToInt32(Session["UserID"]);

            IEnumerable<GeneralRequest> allRequests = function.GetYourAllRequests(userID);
            return View(allRequests);

        }


        [HttpGet]
        public ActionResult GetAreaData()
        {
            IEnumerable<string> AreaDetails = governmentDBContext.UserVoterIDDetails.Select(x=>x.State).Distinct();
          
            return View("SelectArea",AreaDetails);

        }

        [HttpPost]

        public ActionResult GetAreaData(string area)
        {
 
            if (area == "All Area")
            {
                IEnumerable<UserVoterIDDetail> AllVotersfromGivenArea = governmentDBContext.UserVoterIDDetails.ToList<UserVoterIDDetail>();
                ViewBag.count = governmentDBContext.UserVoterIDDetails.ToList<UserVoterIDDetail>().Count();
                return View(AllVotersfromGivenArea);
            }
            else
            {
                IEnumerable<UserVoterIDDetail> AllVotersfromGivenArea = governmentDBContext.GetAllVotersFromArea(area).ToList<UserVoterIDDetail>();
                ViewBag.count = governmentDBContext.GetAllVotersFromArea(area).Count();
                return View(AllVotersfromGivenArea);
            }
            
        }



        [HttpGet]
        public ActionResult AllElectionParties()
        {
            EvotingCommonDBContext context = new EvotingCommonDBContext();
            IEnumerable<ElectionParty> electionParty = context.ElectionParties.ToList();
         return View(electionParty);

        }

          [HttpGet]
        public ActionResult Create()
        {
            return View();
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
          public ActionResult Create(string PartyName, string PartyHead, string PartyHeadOffice, Nullable<bool> IsActive)
          {
              ElectionParty electionParty = new ElectionParty();
              EvotingCommonDBContext context = new EvotingCommonDBContext();
              if (Request.Files["ImageData"] == null)
                  return View();

              HttpPostedFileBase file = Request.Files["ImageData"];


              electionParty.PartyName = PartyName;
              electionParty.PartyHead = PartyHead;
              electionParty.PartyHeadOffice = PartyHeadOffice;
              electionParty.PartyLogo = ConvertToBytes(file);
              electionParty.IsActive = IsActive;

              context.ElectionParties.Add(electionParty);
             int success= context.SaveChanges();
             if (success > 0)
             {
                 Session["UploadPicture"] = "Success";
                
                 IEnumerable<ElectionParty> electionParties = context.ElectionParties.ToList();

                 return View("AllElectionParties", electionParties);
             }
             else
             {
                 Session["UploadPicture"] = "error";
             }

             return View();
          }

       
         [HttpGet]
        public ActionResult Edit(int PartyId)
        {
                 EvotingCommonDBContext context = new EvotingCommonDBContext();
            ElectionParty party = context.ElectionParties.Single(p => p.PartyId == PartyId);

            if (party == null)
            {
                return HttpNotFound();
            }

            ElectionPartyModel electionPartyModel = new ElectionPartyModel();
            electionPartyModel.PartyId = party.PartyId;
            electionPartyModel.PartyName = party.PartyName;
            electionPartyModel.PartyHead = party.PartyHead;
            electionPartyModel.PartyHeadOffice = party.PartyHeadOffice;
            electionPartyModel.PartyLogo = party.PartyLogo;
            return View(electionPartyModel);
              
        }

          [HttpPost]
         
           public ActionResult Edit(int PartyId, string PartyName, string PartyHead, string PartyHeadOffice, Nullable<bool> IsActive)
          {

              EvotingCommonDBContext context = new EvotingCommonDBContext();
              ElectionParty party = context.ElectionParties.Single(p => p.PartyId == PartyId);
              
             

              HttpPostedFileBase file = Request.Files["ImageData"];
              if (ModelState.IsValid)
              {
                  party.PartyName = PartyName;
                  party.PartyHead = PartyHead;
                  party.PartyHeadOffice = PartyHeadOffice;
                  party.IsActive = IsActive;
                  if (file!=null)
                      party.PartyLogo = ConvertToBytes(file);
                  context.SaveChanges();
            IEnumerable<ElectionParty> electionParty = context.ElectionParties.ToList();
                  return RedirectToAction("AllElectionParties");
              }

              ElectionPartyModel electionPartyModel = new ElectionPartyModel();
              electionPartyModel.PartyId = PartyId;
              electionPartyModel.PartyName = PartyName;
              electionPartyModel.PartyHead = PartyHead;
              electionPartyModel.PartyHeadOffice = PartyHeadOffice;
              return View(electionPartyModel);
          }


          [HttpGet]
          public ActionResult AllCandidates()
          {
              EvotingCommonDBContext context = new EvotingCommonDBContext();
              IEnumerable<CandidateMaster> candidateMasterList = context.CandidateMasters.ToList();
              return View(candidateMasterList);
          }

           [HttpGet]
          public ActionResult CreateCandidate()
        {
            return View();
        }

           [HttpPost]
           public ActionResult CreateCandidate(string CandidateName, DateTime DateOfBirth,string CandidateDetails,  Nullable<bool> IsActive)
           {
          
               CandidateMaster candidateMaster = new CandidateMaster();
               EvotingCommonDBContext context = new EvotingCommonDBContext();
               if (Request.Files["ImageData"] == null)
                   return View();

               HttpPostedFileBase file = Request.Files["ImageData"];

               candidateMaster.CandidateName = CandidateName;
               candidateMaster.DateOfBirth = DateOfBirth;
               candidateMaster.CandidateDetails = CandidateDetails;
               candidateMaster.IsActive = IsActive;
               candidateMaster.CandidatePicture = ConvertToBytes(file);

               context.CandidateMasters.Add(candidateMaster);
               int success = context.SaveChanges();
               if (success > 0)
               {
                   IEnumerable<CandidateMaster> candidateMasterList = context.CandidateMasters.ToList();

                   return View("AllCandidates", candidateMasterList);
               }
       
               return View();
           }
         
         
             
         [HttpGet]
           public ActionResult EditCandidate(int CandidateId)
        {
                 EvotingCommonDBContext context = new EvotingCommonDBContext();

                 CandidateMaster candidateMaster = context.CandidateMasters.Single(p => p.CandidateId == CandidateId);

                 if (candidateMaster == null)
                return HttpNotFound();

                return View(candidateMaster);
              
        }

          [HttpPost]

         public ActionResult EditCandidate(int CandidateId, string CandidateName, DateTime DateOfBirth, string CandidateDetails, Nullable<bool> IsActive)
          {
              
              EvotingCommonDBContext context = new EvotingCommonDBContext();
              CandidateMaster candidateMaster = context.CandidateMasters.Single(p => p.CandidateId == CandidateId);
              
              HttpPostedFileBase file = Request.Files["ImageData"];
           
                candidateMaster.CandidateName = CandidateName;
               candidateMaster.DateOfBirth = DateOfBirth;
               candidateMaster.CandidateDetails = CandidateDetails;
               candidateMaster.IsActive = IsActive;
               if (file!=null)
               candidateMaster.CandidatePicture = ConvertToBytes(file);
                int success=  context.SaveChanges();
              if(success>0)
              { 
            IEnumerable<CandidateMaster> masterlist = context.CandidateMasters.ToList();
                  return RedirectToAction("AllCandidates",masterlist);
              }

             return  View(candidateMaster);
        
          }

         
        [HttpGet]
          public ActionResult CandidateForElection()
        {
            EvotingCommonDBContext context = new EvotingCommonDBContext();
            IEnumerable<ElectionCandidate> electionCandidate = context.ElectionCandidates.ToList();
            return View(electionCandidate);

        }

        //Create Candidate For Election
         

        [HttpGet]
        public ActionResult CreateCandidateForElection(int CandidateNo)
        {
            List<SelectListItem> CandidateIDandNames = new List<SelectListItem>();
            CandidateIDandNames = GetAllCandidateIdsAndNames();
            ViewData.Add("CandidateIdAndNames", CandidateIDandNames);
            List<SelectListItem> PartyIDandNames = new List<SelectListItem>();
            PartyIDandNames = GetAllPartyNameAndId();
            ViewData.Add("PartyIdAndNames", PartyIDandNames);
            List<SelectListItem> AreaIdAndNames = new List<SelectListItem>();
            AreaIdAndNames=GetAllAreaNameAndId();
            ViewData.Add("AreaIdAndNames", AreaIdAndNames);
            return View();
        }


        [HttpPost]
        public ActionResult CreateCandidateForElection(int CandidateId,int PartyId,int AreaId,bool IsActive )
        {
            
            ElectionCandidate electionCandidate = new ElectionCandidate();
            EvotingCommonDBContext context = new EvotingCommonDBContext();
            if (Request.Files["ImageData"] == null)
                return View();

            HttpPostedFileBase file = Request.Files["ImageData"];

            electionCandidate.CandidateId = CandidateId;
            electionCandidate.AreaId = AreaId;
            electionCandidate.PartyId = PartyId;
            electionCandidate.IsActive = IsActive;
            electionCandidate.Symbol = ConvertToBytes(file);

            context.ElectionCandidates.Add(electionCandidate);
            int success = context.SaveChanges();
            if (success > 0)
            {
                EvotingCommonDBContext contextnew = new EvotingCommonDBContext();//create a different context class so won't get any error while showing main list after creation
                IEnumerable<ElectionCandidate> electionCandidates = contextnew.ElectionCandidates.ToList();
                return View("CandidateForElection",electionCandidates);
            }

            return View();
        }


        [HttpGet]
        public ActionResult EditCandidateForElection(int CandidateNo)
        {
            EvotingCommonDBContext context = new EvotingCommonDBContext();
            ElectionCandidate electionCandidate = context.ElectionCandidates.Single(p => p.CandidateNo == CandidateNo);

            if (electionCandidate == null)
                return HttpNotFound();
            List<SelectListItem> CandidateIDandNames = new List<SelectListItem>();
            CandidateIDandNames = GetAllCandidateIdsAndNames();
            ViewData.Add("CandidateIdAndNames", CandidateIDandNames);
            List<SelectListItem> PartyIDandNames = new List<SelectListItem>();
            PartyIDandNames = GetAllPartyNameAndId();
            ViewData.Add("PartyIdAndNames", PartyIDandNames);
            List<SelectListItem> AreaIdAndNames = new List<SelectListItem>();
            AreaIdAndNames = GetAllAreaNameAndId();
            ViewData.Add("AreaIdAndNames", AreaIdAndNames);

            return View(electionCandidate);

        }

        [HttpPost]

        public ActionResult EditCandidateForElection(int CandidateNo, int CandidateId, int PartyId, int AreaId, Nullable<bool> IsActive)
        {

            EvotingCommonDBContext context = new EvotingCommonDBContext();
            ElectionCandidate electionCandidate = context.ElectionCandidates.Single(p => p.CandidateNo == CandidateNo);
            HttpPostedFileBase file = Request.Files["ImageData"];


            electionCandidate.CandidateId = CandidateId;
            electionCandidate.AreaId = AreaId;
            electionCandidate.PartyId = PartyId;
            electionCandidate.IsActive = IsActive;
            if (file != null)
                electionCandidate.Symbol = ConvertToBytes(file);
            int success = context.SaveChanges();
            if (success > 0)
            {
               
                IEnumerable<CandidateMaster> masterlist = context.CandidateMasters.ToList();
                return RedirectToAction("CandidateForElection", masterlist);
            }

            return View(electionCandidate);

        }






         //functions to populate data in dropdown list
        public List<SelectListItem> GetAllCandidateIdsAndNames()
         {
             EvotingCommonDBContext context = new EvotingCommonDBContext();
             List<SelectListItem> CandidateIDandName = new List<SelectListItem>();

            CandidateMaster master=new CandidateMaster();
            var allCandidateIDandName = context.CandidateMasters.Select(x => new { x.CandidateName, x.CandidateId});

           foreach (var item in allCandidateIDandName)
           {
               CandidateIDandName.AddRange(new[]{
                            new SelectListItem() { Text=item.CandidateName, Value=item.CandidateId.ToString()}});
           }
             return CandidateIDandName;
         }
        public List<SelectListItem> GetAllPartyNameAndId()
        {
            EvotingCommonDBContext context = new EvotingCommonDBContext();
            List<SelectListItem> PartyIDandName = new List<SelectListItem>();

            ElectionParty electionParty = new ElectionParty();
            var allPartyIDandName = context.ElectionParties.Select(x => new { x.PartyId, x.PartyName });

            foreach (var item in allPartyIDandName)
            {
                PartyIDandName.AddRange(new[]{
                            new SelectListItem() { Text=item.PartyName, Value=item.PartyId.ToString()}});
            }
            return PartyIDandName;
        }

        public List<SelectListItem> GetAllAreaNameAndId()
        {
            EvotingCommonDBContext context = new EvotingCommonDBContext();
            List<SelectListItem> AreaIDandName = new List<SelectListItem>();

            Area area = new Area();
            var allAreaIDandName = context.Areas.Select(x => new { x.AreaId, x.AreaName });

            foreach (var item in allAreaIDandName)
            {
                AreaIDandName.AddRange(new[]{
                            new SelectListItem() { Text=item.AreaName, Value=item.AreaId.ToString()}});
            }
            return AreaIDandName;
        }


    }
}