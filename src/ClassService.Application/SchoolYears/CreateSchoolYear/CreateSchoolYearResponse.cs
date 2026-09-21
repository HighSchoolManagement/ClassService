using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.SchoolYears.CreateSchoolYear
{
    public class CreateSchoolYearResponse
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
