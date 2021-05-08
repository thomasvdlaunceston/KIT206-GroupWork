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
                    res.positions = new List<Researcher.Position>();
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

        private static List<int> getStudentID(int id)
        {
            MySqlDataReader rdr = null;
            List<int> sIDs = new List<int>();

            try
            {
                // Open the connection
                conn.Open();

                // 1. Instantiate a new command with a query and connection
                //MySqlCommand cmd = new MySqlCommand("select type,given_name,family_name,title,unit,campus,email,photo,degree,supervisor_id,level,utas_start,current_start from researcher where id = "+id, conn);
                MySqlCommand cmd = new MySqlCommand("select id, type from researcher where supervisor_id = " + id, conn);

                // 2. Call Execute reader to get query results
                rdr = cmd.ExecuteReader();

                // print the CategoryName of each record
                while (rdr.Read())
                {
                    //This illustrates how the raw data can be obtained using an indexer [] or a particular data type can be obtained using a GetTYPENAME() method.
                    //Console.WriteLine("{0} {1}", rdr[0], rdr.GetString(1));

                    if (rdr.GetString(1) == "Student")
                    {
                        sIDs.Add(rdr.GetInt32(0));
                    }
                    else
                    {
                        System.Environment.Exit(1);
                    }

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
            return sIDs;

        }
        private static List<Researcher.Position> fetchPositions(int id)
        {
            //Fetch Positions
            MySqlDataReader rdr = null;
            GetConnection();
            List<Researcher.Position> positions = new List<Researcher.Position>();

            try
            {
                // Open the connection
                conn.Open();

                // 1. Instantiate a new command with a query and connection
                MySqlCommand cmd = new MySqlCommand("select * from position where id = " + id + " order by start", conn);

                // 2. Call Execute reader to get query results
                rdr = cmd.ExecuteReader();

                // print the CategoryName of each record
                while (rdr.Read())
                {
                    //This illustrates how the raw data can be obtained using an indexer [] or a particular data type can be obtained using a GetTYPENAME() method.
                    //Console.WriteLine("{0} {1}", rdr[0], rdr.GetString(1));
                    //https://stackoverflow.com/questions/20547261/database-field-enum-to-c-sharp-list
                    var enumerated = rdr[1] != DBNull.Value ? rdr.GetString(1) : "Student";
                    Researcher.Position pos = new Researcher.Position { start = rdr.GetDateTime(2), end = rdr.GetDateTime(3), level = ((Researcher.EmploymentLevel)Enum.Parse(typeof(Researcher.EmploymentLevel), enumerated)) };
                    positions.Add(pos);
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
            return positions;
        }
        public static Researcher.Researcher fetchFullResearcherDetails(int id) 
        {

            MySqlDataReader rdr = null;
            List<Researcher.Position> positions;
            List<Researcher.Student> supervisions = new List<Researcher.Student>();
            Researcher.Student student;
            Researcher.Staff staff;
            GetConnection();

            try
            {
                // Open the connection
                conn.Open();

                // 1. Instantiate a new command with a query and connection
                //MySqlCommand cmd = new MySqlCommand("select type,given_name,family_name,title,unit,campus,email,photo,degree,supervisor_id,level,utas_start,current_start from researcher where id = "+id, conn);
                MySqlCommand cmd = new MySqlCommand("select * from researcher where id = " + id, conn);

                // 2. Call Execute reader to get query results
                rdr = cmd.ExecuteReader();

                // print the CategoryName of each record
                while (rdr.Read())
                {
                    //This illustrates how the raw data can be obtained using an indexer [] or a particular data type can be obtained using a GetTYPENAME() method.
                    //Console.WriteLine("{0} {1}", rdr[0], rdr.GetString(1));

                    if (rdr.GetString(2) == "Student")
                    {
                        student = new Researcher.Student { GivenName = rdr.GetString(2), FamilyName = rdr.GetString(3), Title = rdr.GetString(4), School = rdr.GetString(5), Campus = rdr.GetString(6), Email = rdr.GetString(7), Photo = rdr.GetString(8), Degree = rdr.GetString(9) };
                        Researcher.Position studentPos = new Researcher.Position { start = rdr.GetDateTime(12), level = Researcher.EmploymentLevel.Student };
                        student.positions = new List<Researcher.Position>();
                        student.positions.Add(studentPos);
                        return student;
                    }
                    else 
                    {
                        staff = new Researcher.Staff { GivenName = rdr.GetString(2), FamilyName = rdr.GetString(3), Title = rdr.GetString(4), School = rdr.GetString(5), Campus = rdr.GetString(6), Email = rdr.GetString(7), Photo = rdr.GetString(8) };
                        positions = fetchPositions(id);
                        staff.positions = new List<Researcher.Position>(positions);
                        foreach (int iid in getStudentID(id))
                        {
                            supervisions.Add((Researcher.Student)fetchFullResearcherDetails(iid));
                        }
                        staff.student = new List<Researcher.Student>(supervisions);
                        return staff;
                    }

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
            return new Researcher.Researcher();
        }
    }
        /*
        public static Researcher.Researcher completeResearcherDetails(Researcher.Researcher r) { }
        public static Researcher.Publication[] fetchBasicPublicationDetails(Researcher.Researcher r) { }
        public static Researcher.Publication  completePublicationDetails(Researcher.Publication p) { }
        public static int[] fetchPublicationCounts(Date from, Date to) { }*/







}

       