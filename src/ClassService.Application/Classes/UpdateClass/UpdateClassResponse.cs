using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.UpdateClass
{
    public class UpdateClassResponse 
    {
        public int ClassId { get; set; }
        public int SchoolId { get; set; }
        public int SchoolYearId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
