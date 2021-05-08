using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KIT206_GroupWork.Control;

namespace KIT206_GroupWork
{
    class Program
    {
        static void Main(string[] args)
        {
            string response;
            Control.ResearcherController R_Controller = new ResearcherController();
            R_Controller.LoadReseachers();
            do
            {
                R_Controller.basicConsoleDisplay().ForEach(Console.WriteLine);
                Console.WriteLine("Would you like to a) exit, b) filter by name, c) filter by level, d) reset, e) sort ascending, f) sort descending, g) more info");
                response = Console.ReadLine();
                //Should have used switch but anyway :-)
                if (response == "a")
                {
                }
                else if (response == "b")
                {
                    Console.WriteLine("Please enter name: ");
                    R_Controller.FilterByName(Console.ReadLine());
                }
                else if (response == "c")
                {
                    Console.WriteLine("Please enter Employment Level: ");
                    R_Controller.FilterBy((Researcher.EmploymentLevel)Enum.Parse(typeof(Researcher.EmploymentLevel), Console.ReadLine()));
                }
                else if (response == "d")
                {
                    R_Controller.reset();
                }
                else if (response == "e")
                {
                    R_Controller.sortAlphabetically(true);
                }
                else if (response == "f")
                {
                    R_Controller.sortAlphabetically(false);
                }
                else if (response == "g")
                {
                    Console.WriteLine("Please enter ID: ");
                    int id = Int32.Parse(Console.ReadLine());
                    R_Controller.LoadResearcherDetails(id);
                    R_Controller.researcherConsoleDisplay().ForEach(Console.WriteLine);
                    Console.WriteLine("Press enter to return: ");
                    Console.ReadLine();

                }
            } while (response != "a");

          
        }
    }
}
