using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Domain.Entities
{
    public class Class
    {
        public int Id {get;set;}
        public int SchoolId { get; set; }
        public int SchoolYearId {get;set;}
        public string Name { get; set; } = string.Empty;
        public int Capacity {get;set;}
        public DateTime CreatedDate {get;set;}
        public DateTime ModifiedDate {get;set;}
        public SchoolYear SchoolYear { get; set; } = new SchoolYear();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    }
}
