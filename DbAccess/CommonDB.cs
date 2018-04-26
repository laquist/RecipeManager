using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DbAccess
{
    public abstract class CommonDB
    {
        //Fields
        private readonly string connectionString;

        public CommonDB(string conString)
        {
            connectionString = conString;
        }

        public DataSet ExecuteQuery(string query, CommandType cType = CommandType.Text) //Så CommandType er som standard = Text, og jeg behøver ikke at give det med som parameter. MEN hvis jeg bruger en StoredProcedure, så skal jeg give CommandType.StoredProcedure med på en måde. Og det kommer jeg til at skulle fordi at dataset skal nok hente alle tabeller ud.
        {
            //Bliver brugt til at eksekvere inserts på databasen, den modtager SQL-strengen som skal insertes
        }

        public bool ExecuteNonQuery(string query)
        {
            
        }
    }
}
