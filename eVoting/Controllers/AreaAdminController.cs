using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eVoting.Common;
using eVoting.BusinessLayer;
using eVoting.UserLoginModel;
using eVoting.OtherDBContextModel;

namespace eVoting.Controllers
{
     [MyAuthorize(Roles = "AreaAdmin,SuperAdmin")]
    public class AreaAdminController : Controller
    {
        
        // GET: AreaAdmin
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

         public ActionResult AllLocalArea()
         {
             EvotingCommonDBContext context = new EvotingCommonDBContext();
            IEnumerable<Area> areas= context.Areas.ToList();
            return View(areas);
         }

         public ActionResult CreateLocalArea()
         {
             return View();
         }

         [HttpPost]
         public ActionResult CreateLocalArea(string AreaName,string District,string State,Nullable<bool> IsActive)
         {
             EvotingCommonDBContext context = new EvotingCommonDBContext();
             Area area = new Area();
             area.AreaName = AreaName;
             area.District = District;
             area.State = State;
             area.IsActive = IsActive;

             context.Areas.Add(area);
            int success= context.SaveChanges();
            if (success > 0)
            {
                IEnumerable<Area> areas = context.Areas.ToList();
                return View("AllLocalArea",areas);
            }
            return View();

         }


         [HttpGet]
         public ActionResult Edit(int AreaId)
         {
             EvotingCommonDBContext context = new EvotingCommonDBContext();
             Area area = context.Areas.Single(p => p.AreaId == AreaId);

             if (area == null)
             {
                 return HttpNotFound();
             }


             return View(area);

         }

          [HttpPost]
         
           public ActionResult Edit(int AreaId, string AreaName, string District, string State, Nullable<bool> IsActive)
          {

              EvotingCommonDBContext context = new EvotingCommonDBContext();
              Area area = context.Areas.Single(p => p.AreaId == AreaId);

                area.AreaName = AreaName;
             area.District = District;
             area.State = State;
             area.IsActive = IsActive;
               int success = context.SaveChanges();
              if(success>0)
              {
            IEnumerable<ElectionParty> electionParty = context.ElectionParties.ToList();
            return RedirectToAction("AllLocalArea");
              }

              return View(area);
          }

    }
    }
