﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using MySql.Data.Types;


namespace KIT206_GroupWork.Adapters
{
    class ERDAdapter
    {

        public static Researcher.Researcher[] fetchBasicResearcherDetails() {}
        public static Researcher.Researcher fetchFullResearcherDetails(int id) { }
        public static Researcher.Researcher completeResearcherDetails(Researcher.Researcher r) { }
        public static Researcher.Publication[] fetchBasicPublicationDetails(Researcher.Researcher r) { }
        public static Researcher.Publication  completePublicationDetails(Researcher.Publication p) { }
        public static int[] fetchPublicationCounts(Date from, Date to) { }







    }
}
       