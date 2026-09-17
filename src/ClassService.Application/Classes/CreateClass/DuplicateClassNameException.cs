using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.CreateClass
{
    public class DuplicateClassNameException : Exception
    {
        public DuplicateClassNameException(): base("A class with this name already exists in this school year")
        {

        }
    }
}
