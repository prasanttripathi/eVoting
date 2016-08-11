using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace GovernmentDBModels
{
  public class GovernmentFilesDBContext : DbContext
    {


        //we need to create a DBContext object that will be responsible for performing all the CRUD operations on these models.
      public DbSet<UserVoterIDDetails> UservoteridDetails { get; set; }
      public DbSet<UserMarksheetDetails> UsermarksheetDetails { get; set; }



            /*The important thing to notice here is that the name of the connectionString is same as the DbContext class 
          that we have created. If we keep the name of the connectionString same as the DbContext class
              the corresponding DbContext class will use the connectionString to persist the data.
                  This is on the lines of "Convention over configurations".
                  But this is also flexible so that we have the possiblity of giving custom names to the connectionStrings i.e.
                      If we need to give any other name to the connectionString or use some already defined
                          connectionString with the DbContext class then we need to pass the connectionString name 
                              in the base class constructor of our DbContext class. */
        }
}
