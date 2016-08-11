using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using eVoting.ViewModels;


namespace eVoting
{

    
  public  class EvotingDBContext :DbContext
    {

      public DbSet<UserLoginDetails> userDetails { get; set; }

     
    }
}
