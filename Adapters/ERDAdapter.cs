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

            MySqlDataReader rdr = null;
            List<Researcher.Researcher> researcherList = new List<Researcher.Researcher>();
            GetConnection();

            try
            {
                // Open the connection
                conn.Open();

                // 1. Instantiate a new command with a query and connection
                MySqlCommand cmd = new MySqlCommand("select id, given_name, family_name, level, title from researcher", conn);

                // 2. Call Execute reader to get query results
                rdr = cmd.ExecuteReader();

                // print the CategoryName of each record
                while (rdr.Read())
                {
                    //This illustrates how the raw data can be obtained using an indexer [] or a particular data type can be obtained using a GetTYPENAME() method.
                    //Console.WriteLine("{0} {1}", rdr[0], rdr.GetString(1));
                    Researcher.Researcher res = new Researcher.Researcher { GivenName = rdr.GetString(1), FamilyName = rdr.GetString(2), ID = rdr.GetInt32(0), Title=rdr.GetString(4)};
                    //https://stackoverflow.com/questions/20547261/database-field-enum-to-c-sharp-list
                    var enumerated = rdr[3] != DBNull.Value ? rdr.GetString(3) : "Student";
                    Researcher.Position pos = new Researcher.Position { level = (Researcher.EmploymentLevel) Enum.Parse(typeof(Researcher.EmploymentLevel), enumerated)};
                    res.positions.Add(pos);
                    //Employee e = new Employee { Name = combined, ID = rdr.GetInt32(2) };
                    researcherList.Add(res);
                }
            }
            finally
            {
                // close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return researcherList.ToArray();

        }
        /*public static Researcher.Researcher fetchFullResearcherDetails(int id) { }
        public static Researcher.Researcher completeResearcherDetails(Researcher.Researcher r) { }
        public static Researcher.Publication[] fetchBasicPublicationDetails(Researcher.Researcher r) { }
        public static Researcher.Publication  completePublicationDetails(Researcher.Publication p) { }
        public static int[] fetchPublicationCounts(Date from, Date to) { }*/







    }
}
       