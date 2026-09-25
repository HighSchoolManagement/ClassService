using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Domain.Entities
{
    public class SchoolYear
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<Class> Classes { get; set; } = new List<Class>();
    }
}
