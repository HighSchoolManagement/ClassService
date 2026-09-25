using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Domain.Entities
{
    public class ClassSubject
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public bool IsActive { get; set; }
        public Class Class { get; set; } = new Class();
        public Subject Subject { get; set; } = new Subject();
    }
}
