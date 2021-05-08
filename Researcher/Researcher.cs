using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_GroupWork.Researcher
{
    class Researcher
    {
        //researcher details
        public int ID { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Title { get; set; }
        public string School { get; set; }
        public string Campus { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        

        public List<Position> positions { get; set; }
        public List<Publication> publications { get; set; }

        public Researcher()
        {
            //publications = new List<Publication>();
            //positions = new List<Position>();
        }

        public Position GetCurrentJob() 
        {
            return positions.LastOrDefault();
        }
        public string CurrentJobTitle() 
        {
            return positions.LastOrDefault().title();
        }
        public DateTime CurrentJobStart() 
        {
            return positions.LastOrDefault().start;
        }
        public Position EarliestJob()
        {
            return positions.FirstOrDefault();
        }
        public float Tenure() {

            return (float)((DateTime.Now - EarliestJob().start).Days/365.25);
        
        }
        public int PublicationsCount()
        {
            return publications.Count;
        }


    }
}
