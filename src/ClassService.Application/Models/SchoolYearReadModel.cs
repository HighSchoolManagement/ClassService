using ClassService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Models
{
    public class SchoolYearReadModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool IsActive { get; set; }
        public List<ClassReadModel> Class { get; set; } = new List<ClassReadModel>();

    }
}
