using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_Console_application_
{
    public enum EmploymentLevel{student, A, B, C, D, E};
    class Reasercher
    {
        //researcher details
        public int ID { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Title { get; set; }
        public string School { get; set; }
        public string Campus { get; set; }
        public string Email { get; set; }

        public void GetCurrentJob() { }
        public void CurrentJobTitle() { }
        public void CurrentJobStart() { }
        public void EarliestStart() { }
        public float Tenure() {
            return 4;
        
        }
        public int PublicationsCount() {
            return 5;
                }


    }
}
