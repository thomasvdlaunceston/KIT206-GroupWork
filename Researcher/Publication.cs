using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_GroupWork.Researcher
{
	public class Publication
	{
		string DOI { get; set; }
		string Title { get; set; }
		string Authors { get; set; }
		public DateTime Year { get; set; }
		OutputType Type { get; set; }
		string CiteAs { get; set; }
		DateTime Available { get; set; }

        public Publication()
		{

		}

		public int Age()
        {
			//Is this appropriate???
			return DateTime.Now.Year - Year.Year;
        }
	}
}
