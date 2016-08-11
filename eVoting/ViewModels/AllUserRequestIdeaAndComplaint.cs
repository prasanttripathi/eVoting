using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eVoting.UserLoginModel;
using eVoting.OtherDBContextModel;
using System.ComponentModel.DataAnnotations;
namespace eVoting.ViewModels
{
    public class AllUserRequestIdeaAndComplaint
    {

        public IEnumerable<GeneralRequest> AllRequestList { set; get; }
        public IEnumerable<Idea> AllIdeasLists { set; get; }
        public IEnumerable<UserComplaints> AllComplaintsList { set; get; }
    
    }
}