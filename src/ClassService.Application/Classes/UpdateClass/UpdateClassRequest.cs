using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.UpdateClass
{
    public class UpdateClassRequest
    {
        public int SchoolId { get; set; }
        public int SchoolYearId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
}
