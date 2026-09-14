using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.SchoolYears.CreateSchoolYear
{
    public class DuplicateSchoolYearNameException : Exception
    {
        public DuplicateSchoolYearNameException(): base("A school year with this name already exists")
        {

        }
    }
}
