namespace ClassService.Domain.Entities
{
    public class ClassSubject
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }   // cot DB: UpdatedDate
        public Class Class { get; set; } = null!;
        public Subject Subject { get; set; } = null!;
        public ICollection<RoomSchedule> RoomSchedules { get; set; } = new List<RoomSchedule>();
        public ICollection<ClassSubjectTeacher> ClassSubjectTeachers { get; set; } = new List<ClassSubjectTeacher>();
    }
}
