using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Models
{
    public class PeriodReadModel
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public int SchoolYearId { get; set; }
        public int Number { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsActive { get; set; }
    }
}
