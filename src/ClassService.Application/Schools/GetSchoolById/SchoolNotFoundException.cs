using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Schools.GetSchoolById
{
    public class SchoolNotFoundException : Exception
    {
        public SchoolNotFoundException(): base("School not found")
        {

        }
    }
}
