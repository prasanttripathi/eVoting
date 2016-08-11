using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eVoting.Common;
using eVoting.UserLoginModel;
using eVoting.BusinessLayer;
using eVoting.OtherDBContextModel;
using System.Threading;

namespace eVoting.Controllers
{
    [MyAuthorize(Roles = "SuperAdmin")]
    public class AdminDashboardController : Controller
    {
        // GET: AdminDashboard

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

        public ActionResult GetReports()
        {
            GovernmentDBContext governmentDBContext = new GovernmentDBContext();
            int? result1 = governmentDBContext.GetTotalNumberOfVoters().FirstOrDefault();
            ViewBag.Result1 = result1;
            int? result2 = governmentDBContext.GetTotalNumberOfMarksheets().FirstOrDefault();
            ViewBag.Result2 = result2;
            int? result3 = governmentDBContext.TotalRegisteredUsers().FirstOrDefault();
            ViewBag.Result3 = result3;

            //to get all number of admins



            UserLoginDBContext userLoginDBContext = new UserLoginDBContext();
            string[] adminCount = new string[3];
            adminCount[0] = userLoginDBContext.UserLoginDetails.Count(x => x.UserTypeID == 2).ToString();
            adminCount[1] = userLoginDBContext.UserLoginDetails.Count(x => x.UserTypeID == 3).ToString();
            adminCount[2] = userLoginDBContext.UserLoginDetails.Count(x => x.UserTypeID == 4).ToString();
            ViewBag.Result4 = adminCount;


            return View("Report");
        }

        [HttpGet]
        public ActionResult InitiateElection()
        {
            EvotingCommonDBContext context = new EvotingCommonDBContext();
            IList<Area> area = context.Areas.ToList();
            return View(area);
        }

        [HttpPost]
        public ActionResult InitiateElection(IList<Area> AreaDetails)
        {

            EvotingCommonDBContext context = new EvotingCommonDBContext();

            int count=0;
            IList<Area> areaList = AreaDetails;//area1 area2 area3
            if (areaList.Count > 0)
           {
               for (int i = 0; i < areaList.Count; i++)
            {
             int AreaId=   areaList[i].AreaId;
             Area area = context.Areas.Single(p => p.AreaId == AreaId);
              area.ElectionProcess=  areaList[i].ElectionProcess;                  
                 context.SaveChanges();
                 count += 1;
            }
           }
            if (count > 0)
                ViewBag.Message = "Success";
            
               IList < Area > AreaList = context.Areas.ToList();
               return View(AreaList);
        }

      
        public ActionResult StartStopElection(int AreaId)
        {
            EvotingCommonDBContext context = new EvotingCommonDBContext();

            Area area = context.Areas.Single(p => p.AreaId == AreaId);
            if (area.ElectionProcess==false)
            area.ElectionProcess = true;
            else
                area.ElectionProcess = false;
            context.SaveChanges();
            IList<Area> areaList = context.Areas.ToList();
            return View("InitiateElection", areaList);
        }
    }
}