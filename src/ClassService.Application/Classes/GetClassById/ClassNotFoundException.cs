using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.GetClassById
{
    public class ClassNotFoundException : Exception
    {
        public ClassNotFoundException(): base("Class Not Found")
        {

        }
    }
}
