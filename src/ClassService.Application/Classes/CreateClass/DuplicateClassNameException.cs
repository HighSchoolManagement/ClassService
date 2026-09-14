using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.CreateClass
{
    public class DuplicateSchoolYearNameException : Exception
    {
        public DuplicateSchoolYearNameException(): base("A class with this name already exists in this school year")
        {

        }
    }
}
