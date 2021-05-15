using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace KIT206_GroupWork.Control
{
    class ResearcherController
    {
        List<Researcher.Researcher> mainList;
        ObservableCollection<Researcher.Researcher> displayList;
        Researcher.Student student;
        Researcher.Staff staff;
        bool isStaff;


        public ResearcherController()
        {
            
            

        }

        public void LoadReseachers() {
            mainList = new List<Researcher.Researcher>(Adapters.ERDAdapter.fetchBasicResearcherDetails());
            displayList = new ObservableCollection<Researcher.Researcher>(mainList);
            /*string text = ("test");
            Console.WriteLine(text);*/
        }

        public void FilterBy(Researcher.EmploymentLevel level) 
        {
            var selected = from Researcher.Researcher res in mainList
                           where res.GetCurrentJob().level == level
                           select res;
            displayList.Clear();
            selected.ToList().ForEach(displayList.Add);
        }

        public void FilterByName(string name) {
            //Only Filters BY FIRST NAME
            var filtered = from Researcher.Researcher res in mainList
                           where res.GivenName == name
                           select res;
            displayList.Clear();
            filtered.ToList().ForEach(displayList.Add);
        }

        public void sortAlphabetically(Boolean Ascending)
        {
            /*https://stackoverflow.com/questions/29705089/sort-result-alphabetically-in-linq-to-sql*/
            if (Ascending)
            {
                var sorted = from Researcher.Researcher res in mainList
                             orderby res.FamilyName ascending
                             select res;
                displayList.Clear();
                sorted.ToList().ForEach(displayList.Add);
            }
            else if(!Ascending)
            {
                var sorted = from Researcher.Researcher res in mainList
                             orderby res.FamilyName descending
                             select res;
                displayList.Clear();
                sorted.ToList().ForEach(displayList.Add);
            }

        }

        public void reset() 
        {
            displayList.Clear();
            mainList.ForEach(displayList.Add);
        }

        public ObservableCollection<Researcher.Researcher> GetViewableList()
        {
            return displayList;
        }

        public List<String> basicConsoleDisplay()
        {
            List<String> display = new List<string>();
            foreach (Researcher.Researcher res in displayList.ToList())
            {
                display.Add(String.Format("ID: {3}   {0}, {1} ({2})", res.FamilyName, res.GivenName, res.Title, res.ID));
            }
            return display;
        }
        public void LoadResearcherDetails(int id)
        {
            var filtered = from Researcher.Researcher res in mainList
                           where res.ID == id
                           select res;
            if (filtered.ToList().LastOrDefault().GetCurrentJob().level == Researcher.EmploymentLevel.Student)
            {
                student = (Researcher.Student)Adapters.ERDAdapter.fetchFullResearcherDetails(id);
                isStaff = false;
            }
            else
            {
                staff = (Researcher.Staff)Adapters.ERDAdapter.fetchFullResearcherDetails(id);
                isStaff = true;
            }         
        }

        public List<String> researcherConsoleDisplay()
        {
            List<String> display = new List<string>();
            String end;
            if (isStaff)
            {
                display.Add(String.Format("Name: {0} {1}", staff.GivenName, staff.FamilyName));
                display.Add(String.Format("Title: {0}", staff.Title));
                display.Add(String.Format("Unit: {0}", staff.School));
                display.Add(String.Format("Campus: {0}", staff.Campus));
                display.Add(String.Format("Email: {0}", staff.Email));
                display.Add(String.Format("Current Job: {0}", staff.CurrentJobTitle()));
                display.Add(String.Format("Commenced with Institution: {0}", staff.EarliestJob()));
                display.Add(String.Format("Commenced Current Position: {0}", staff.CurrentJobStart()));
                display.Add("Previous positions:");
                foreach (Researcher.Position pos in staff.positions)
                {
                    end = pos.end != pos.start ? "" + pos.end : "present";
                    display.Add(String.Format("{0}    {1}    {2}", pos.start, end, pos.title()));
                    //rdr[3] != DBNull.Value ? rdr.GetDateTime(3): start;
                }
                display.Add(String.Format("Tenure: {0}", staff.Tenure()));
                display.Add("Supervisions:");
                foreach (Researcher.Student stud in staff.student)
                {
                    display.Add(String.Format("{0} {1}", stud.GivenName, stud.FamilyName));
                }
            }
            else
            {
                display.Add(String.Format("Name: {0} {1}", student.GivenName, student.FamilyName));
                display.Add(String.Format("Title: {0}", student.Title));
                display.Add(String.Format("Unit: {0}", student.School));
                display.Add(String.Format("Campus: {0}", student.Campus));
                display.Add(String.Format("Email: {0}", student.Email));
                display.Add(String.Format("Current Job: {0}", student.CurrentJobTitle()));
                display.Add(String.Format("Commenced with Institution: {0}", student.EarliestJob().start));
                display.Add(String.Format("Commenced Current Position: {0}", student.CurrentJobStart()));
                display.Add("Previous positions:");
                foreach (Researcher.Position pos in student.positions)
                {
                    display.Add(String.Format("{0}    {1}    {2}", pos.start, pos.end, pos.title()));
                }
                display.Add(String.Format("Tenure: {0}", student.Tenure()));
                display.Add(String.Format("Degree: {0}", student.Degree));
            }



            return display;
        }
        
    }
}
