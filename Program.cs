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
            Control.PublicationsController P_controller = new PublicationsController();
            Researcher.Researcher res;
            int id;
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
                    id = Int32.Parse(Console.ReadLine());
                    R_Controller.LoadResearcherDetails(id);
                    R_Controller.researcherConsoleDisplay().ForEach(Console.WriteLine);
                    Console.WriteLine("Press a) to exit or b) to display publications: ");
                    if (Console.ReadLine() == "b")
                    {
                        if (R_Controller.isStaff)
                        {
                            res = (Researcher.Researcher)R_Controller.staff;
                        }
                        else
                        {
                            res = (Researcher.Researcher)R_Controller.student;
                        }
                        P_controller.loadPublications(res);
                        P_controller.basicPublicationConsole().ForEach(Console.WriteLine);
                        Console.WriteLine("Press a) to exit or b) to view detailed publication");
                        if (Console.ReadLine() == "b")
                        {
                            Console.WriteLine("Please enter number: ");
                            id = int.Parse(Console.ReadLine());
                            P_controller.loadFullPublications(P_controller.displayList.ToArray()[id]);
                            P_controller.fullPublicationConsole().ForEach(Console.WriteLine);
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                        }
                    }

                }
            } while (response != "a");

          
        }
    }
}
