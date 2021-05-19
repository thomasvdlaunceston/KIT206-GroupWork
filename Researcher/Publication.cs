using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_GroupWork.Researcher
{
	public class Publication
	{
		public string DOI { get; set; }
		public string Title { get; set; }
		public string Authors { get; set; }
		public int Year { get; set; }
		public OutputType Type { get; set; }
		public string CiteAs { get; set; }
		public DateTime Available { get; set; }

        public Publication()
		{

		}

		public int Age()
        {
			//Is this appropriate???
			return DateTime.Now.Year - Year;
        }
	}
}
