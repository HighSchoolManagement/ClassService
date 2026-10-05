namespace ClassService.Domain.Entities
{
    public class ClassSubjectTeacher
    {
        public int Id { get; set; }
        public int ClassSubjectId { get; set; }
        public int TeacherId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }   // cot DB: UpdatedDate
        public ClassSubject ClassSubject { get; set; } = null!;
    }
}
