using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using MySql.Data.Types;


namespace KIT206_GroupWork.Adapters
{
    abstract class ERDAdapter
    {
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";
        private static MySqlConnection conn = null;

        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                string connectionString =
                String.Format("Database={0};Data Source={1};User Id={2}; Password={3}",
                db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        public static Researcher.Researcher[] fetchBasicResearcherDetails() 
        {

        }
        public static Researcher.Researcher fetchFullResearcherDetails(int id) { }
        public static Researcher.Researcher completeResearcherDetails(Researcher.Researcher r) { }
        public static Researcher.Publication[] fetchBasicPublicationDetails(Researcher.Researcher r) { }
        public static Researcher.Publication  completePublicationDetails(Researcher.Publication p) { }
        public static int[] fetchPublicationCounts(Date from, Date to) { }







    }
}
       