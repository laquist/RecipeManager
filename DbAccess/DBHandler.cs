using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess
{
    public class DBHandler : CommonDB
    {
        public DBHandler(string conString) : base (conString) //Smider den det automatisk videre til base class (altså CommonDB), eller skal jeg også have et connectionString field i denne klasse?
        {
            
        }


    }
}
