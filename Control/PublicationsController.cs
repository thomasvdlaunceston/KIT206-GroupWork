using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace KIT206_GroupWork.Control
{
    class PublicationsController
    {
        public List<Researcher.Publication> mainList;
        public ObservableCollection<Researcher.Publication> displayList;
        Researcher.Publication publication;

        public void loadPublications(Researcher.Researcher r)
        {
            mainList = new List<Researcher.Publication>(loadPublicationsFor(r));
            displayList = new ObservableCollection<Researcher.Publication>(mainList);
        }

        public void loadFullPublications(Researcher.Publication p)
        {
            publication = loadPublicationDetails(p);
        }

        public List<string> basicPublicationConsole()
        {
            List<string> console = new List<string>();
            int counter = 0;
            foreach (Researcher.Publication pub in displayList.ToList())
            {
                
                console.Add(String.Format("{2}: {0}, {1}", pub.Year, pub.Title, counter));
                counter += 1;
            }
            return console;
        }

        public List<String> fullPublicationConsole()
        {
            List<String> display = new List<string>();
           
            display.Add(String.Format("DOI: {0}", publication.DOI));
            display.Add(String.Format("Title: {0}", publication.Title));
            display.Add(String.Format("Authors: {0}", publication.Authors));
            display.Add(String.Format("Publication Year: {0}", publication.Authors));
            display.Add(String.Format("Type: {0}", publication.Type));
            display.Add(String.Format("Cite As: {0}", publication.CiteAs));
            display.Add(String.Format("Availability Date: {0}", publication.Available));
            display.Add(String.Format("Age: {0}", publication.Age()));
            return display;
        }

        public Researcher.Publication[]  loadPublicationsFor(Researcher.Researcher r) {
            return Adapters.ERDAdapter.fetchBasicPublicationDetails(r);
        }

        public Researcher.Publication loadPublicationDetails(Researcher.Publication p)
        {
            return Adapters.ERDAdapter.completePublicationDetails(p);
        }
    }
}
