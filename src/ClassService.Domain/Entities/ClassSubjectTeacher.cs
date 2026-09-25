using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Domain.Entities
{
    public class ClassSubjectTeacher
    {
        public int Id { get; set; }
        public int ClassSubjectId { get; set; }
        public int TeacherId { get; set; }
        public bool IsActive { get; set; }
        public ClassSubjectTeacher ClassSubjectTeachers { get; set; } = new ClassSubjectTeacher();
    }
}
