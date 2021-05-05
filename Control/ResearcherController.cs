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
        Researcher.Researcher researcher;


        public ResearcherController()
        {
            mainList = new List<Researcher.Researcher>(LoadReseachers());
            displayList = new ObservableCollection<Researcher.Researcher>(mainList);

        }

        public Researcher.Researcher[] LoadReseachers() {
            return Adapters.ERDAdapter.fetchBasicResearcherDetails();
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
            var filtered = from Researcher.Researcher x in mainList
                           where x.GivenName == name
                           select x;
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
        public void LoadResearcherDetails(int id)
        {
            researcher = Adapters.ERDAdapter.fetchFullResearcherDetails(id);
        }
    }
}
