using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eVoting.UserLoginModel;
using eVoting.Models;
using eVoting.BusinessLayer;

namespace eVoting.Controllers
{
    public class RequestController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendRequest(string requestedby, string requesterEmail, string requestedto, string requestArea, string requestReason)
        {
            GeneralRequest generalRequest = new GeneralRequest();
            functions function = new functions();

            generalRequest.RequestNumber = function.GenerateRequestNumber();
           generalRequest.Requestedby = requestedby;
           generalRequest.RequesterEmail = requesterEmail;
           generalRequest.Requestedto = requestedto;
           generalRequest.RequestArea = requestArea;
           generalRequest.RequestReason = requestReason;
           generalRequest.status = "Pending";

           GovernmentDBContext governmentDBContext = new GovernmentDBContext();
           governmentDBContext.GeneralRequests.Add(generalRequest);
         int i=  governmentDBContext.SaveChanges();
         if (i > 0)
         {
             ViewData["PartialViewDisplay"] = "Yes";
             ViewBag.RequestSubmitted = generalRequest.RequestNumber;
             return View("Unauthorized");
         }
            else

         {
             ViewData["PartialViewDisplay"] = "No";
             ViewBag.RequestSubmitted =null;
             return View();
         }
            

           
        }


    }
}