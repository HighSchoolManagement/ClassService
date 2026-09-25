using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Domain.Entities
{
    public class ClassTeacherAssignment
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public bool IsHomeRoomTeacher { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<Class> Classes { get; set; } = new List<Class>();
    }
}
