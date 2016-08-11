using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GovernmentDBModels;
using System.Runtime.InteropServices;
using PagedList;
using PagedList.Mvc;
using eVoting.Common;

namespace eVoting.Controllers
{
     [MyAuthorize(Roles = "SuperAdmin,MainAdmin")]
    public class GovernmentAdminController : Controller
    {

        GovernmentFilesDBContext context = new GovernmentFilesDBContext();

       
        public ActionResult CentralHome()
        {
            return View();
        }


        //public ActionResult Index()
        //{           
        //    List<UserVoterIDDetails> userVoterIDDetails = context.UservoteridDetails.ToList();
        //    return View(userVoterIDDetails);   
        //}
      
        public ActionResult Index(string SearchBy, string search, int? page)
        {

            if (SearchBy == "VoterId")
            {
                return View(context.UservoteridDetails.Where(x => x.VoterId.StartsWith(search)  || search == null).ToList().ToPagedList(page ?? 1, 5));
            }
            else
            {
                return View(context.UservoteridDetails.Where(x => x.Name.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 5));
            }
           
        }


        public ActionResult Index1(string SearchBy, string search, int? page)
        {
            if (SearchBy == "MarksheetId")
            {
                return View(context.UsermarksheetDetails.Where(x => x.MarksheetId.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 5));
            }
            else
            {
                return View(context.UsermarksheetDetails.Where(x => x.Name.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 5));
            }

        }
        public ActionResult Details(string Id)
        {
            UserVoterIDDetails userVoterIDDetails = context.UservoteridDetails.SingleOrDefault(b => b.VoterId == Id);

            if (userVoterIDDetails == null)
            {
                return HttpNotFound();
            }
            return View(userVoterIDDetails);
        }


        public ActionResult DetailsMarksheets(string Id)
        {
            UserMarksheetDetails userMarksheetDetails = context.UsermarksheetDetails.SingleOrDefault(b => b.MarksheetId == Id);

            if (userMarksheetDetails == null)
            {
                return HttpNotFound();
            }
            return View(userMarksheetDetails);
        }

        public ActionResult Create()
        {
            return View();
        }

       

        [HttpPost]
        public ActionResult Create(UserVoterIDDetails userVoterIDDetails)
        {
            if (ModelState.IsValid)
            {
                context.UservoteridDetails.Add(userVoterIDDetails);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userVoterIDDetails);
        }

        public ActionResult CreateMarksheets()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMarksheets(UserMarksheetDetails userMarksheetDetails)
        {
            if (ModelState.IsValid)
            {
                context.UsermarksheetDetails.Add(userMarksheetDetails);
                context.SaveChanges();
                return RedirectToAction("Index1");
            }

            return View(userMarksheetDetails);
        }


        
        public ActionResult Edit(string Id)
        {
            UserVoterIDDetails userVoterIDDetails = context.UservoteridDetails.Single(p => p.VoterId == Id);
            if (userVoterIDDetails == null)
            {
                return HttpNotFound();
            }
            return View(userVoterIDDetails);
        }


        [HttpPost]
        
        public ActionResult Edit(string Id, UserVoterIDDetails userVoterIDDetails)
        {
            UserVoterIDDetails _userVoterIDDetails = context.UservoteridDetails.Single(p => p.VoterId == Id);

            if (ModelState.IsValid)
            {
                _userVoterIDDetails.Name = userVoterIDDetails.Name;
                _userVoterIDDetails.FathersName = userVoterIDDetails.FathersName;
                _userVoterIDDetails.DateOfBirth = userVoterIDDetails.DateOfBirth;
                _userVoterIDDetails.Address = userVoterIDDetails.Address;
                _userVoterIDDetails.District = userVoterIDDetails.District;
                _userVoterIDDetails.State = userVoterIDDetails.State;

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userVoterIDDetails);
        }

        public ActionResult EditMarksheets(string Id)
        {
            UserMarksheetDetails userMarksheetDetails = context.UsermarksheetDetails.Single(p => p.MarksheetId == Id);
            if (userMarksheetDetails == null)
            {
                return HttpNotFound();
            }
            return View(userMarksheetDetails);
        }

        [HttpPost]
        public ActionResult EditMarksheets(string Id, UserMarksheetDetails userMarksheetDetails)
        {
            UserMarksheetDetails _userMarksheetDetails = context.UsermarksheetDetails.Single(p => p.MarksheetId == Id);

            if (ModelState.IsValid)
            {
                _userMarksheetDetails.Name = userMarksheetDetails.Name;
                _userMarksheetDetails.FathersName = userMarksheetDetails.FathersName;
                _userMarksheetDetails.DateOfBirth = userMarksheetDetails.DateOfBirth;
                _userMarksheetDetails.Marksheettype = userMarksheetDetails.Marksheettype;
               
                context.SaveChanges();
                return RedirectToAction("Index1");
            }
            return View(userMarksheetDetails);
        }

          [MyAuthorize(Roles = "SuperAdmin")]
        public ActionResult Delete(string Id)
        {
            UserVoterIDDetails userVoterIDDetails = context.UservoteridDetails.Single(p => p.VoterId == Id);
            if (userVoterIDDetails == null)
            {
                return HttpNotFound();
            }
            return View(userVoterIDDetails);
        }

        [HttpPost]
        [MyAuthorize(Roles = "SuperAdmin")]
        public ActionResult Delete(string Id, UserVoterIDDetails userVoterIDDetails)
        {
            UserVoterIDDetails _userVoterIDDetails = context.UservoteridDetails.Single(p => p.VoterId == Id);
            context.UservoteridDetails.Remove(_userVoterIDDetails);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

          [MyAuthorize(Roles = "SuperAdmin")]
        public ActionResult DeleteMarksheets(string Id)
        {
            UserMarksheetDetails userMarksheetDetails = context.UsermarksheetDetails.Single(p => p.MarksheetId == Id);
            if (userMarksheetDetails == null)
            {
                return HttpNotFound();
            }
            return View(userMarksheetDetails);
        }

        [HttpPost]
        [MyAuthorize(Roles = "SuperAdmin")]
        public ActionResult DeleteMarksheets(string Id, UserVoterIDDetails userVoterIDDetails)
        {
            UserMarksheetDetails _userMarksheetDetails = context.UsermarksheetDetails.Single(p => p.MarksheetId == Id);
            context.UsermarksheetDetails.Remove(_userMarksheetDetails);
            context.SaveChanges();
            return RedirectToAction("Index1");
        }



        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult GetVoterNameSuggestions(string term, string Id)
        {
            if (Id == "Name")
            { 
            List<string> voterIdDetails = context.UservoteridDetails.Where(s => s.Name.StartsWith(term))
                .Select(x => x.Name).ToList();
            return Json(voterIdDetails, JsonRequestBehavior.AllowGet);
            }
            else if (Id == "VoterId")
            {
                List<string> voterIdDetails = context.UservoteridDetails.Where(s => s.VoterId.StartsWith(term))
                .Select(x => x.VoterId).ToList();
                return Json(voterIdDetails, JsonRequestBehavior.AllowGet);
            }
            else 
            { 
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
         /*
         
         @*@section Scripts {
    <link href="http://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet">
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>

   <script type="text/javascript">
       $(function () {
           $("#search").autocomplete({
               source: '@Url.Action("GetUserMarksheetsSuggestions")',
               minLength: 1
           });
       });

</script>

}*@
         
         */
        public JsonResult GetUserMarksheetsSuggestions(string term, string Id)
        {
            if (Id == "Name")
            {
                List<string> marksheetDetails = context.UsermarksheetDetails.Where(s => s.Name.StartsWith(term))
                    .Select(x => x.Name).ToList();
                return Json(marksheetDetails, JsonRequestBehavior.AllowGet);
            }
            else if (Id == "MarksheetId")
            {
                List<string> marksheetDetails = context.UsermarksheetDetails.Where(s => s.MarksheetId.StartsWith(term))
                .Select(x => x.MarksheetId).ToList();
                return Json(marksheetDetails, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
           
           
        }
    }
}