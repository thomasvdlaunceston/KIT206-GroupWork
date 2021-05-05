using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_GroupWork.Researcher
{
	class Staff: Researcher
	{
		private Dictionary<EmploymentLevel, double> expectedPub = new Dictionary<EmploymentLevel, double>();
		private List<Student> student;

		public Staff()
		{
			student = new List<Student>();
			//I understand that students do not have an expected number of publications but for preventing errors setting to 0
			expectedPub[EmploymentLevel.Student] = 0;
			expectedPub[EmploymentLevel.A] = 0.5;
			expectedPub[EmploymentLevel.B] = 1;
			expectedPub[EmploymentLevel.C] = 2;
			expectedPub[EmploymentLevel.D] = 3.2;
			expectedPub[EmploymentLevel.E] = 4;
		}

		public float ThreeYearAverage()
		{
			int numberOfPublications = 0;
			int CurrentYear = DateTime.Now.Year;
			int totalPublications = publications.Count;

			while ((CurrentYear - publications[totalPublications].Year.Year)<=3)
            {
				numberOfPublications++;
				totalPublications--;
            }

			return numberOfPublications / 3;
			
		}

		public float Performance()
        {
			double CurrentExpectedNumber = expectedPub[GetCurrentJob().level];
			return (float)(ThreeYearAverage() / CurrentExpectedNumber);
        }



	}
}
