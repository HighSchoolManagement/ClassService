using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.SchoolYears.GetSchoolYearById
{
    public class SchoolYearNotFoundException : Exception
    {
        public SchoolYearNotFoundException(): base("School Year not found")
        {

        }
    }
}
