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
              Control.ResearcherController R_Controller = new ResearcherController();
              R_Controller.LoadReseachers();
              Console.WriteLine();
          
        }
    }
}
