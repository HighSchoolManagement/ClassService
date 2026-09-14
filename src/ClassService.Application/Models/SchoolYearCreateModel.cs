using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Models
{
    public class SchoolYearCreateModel
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
