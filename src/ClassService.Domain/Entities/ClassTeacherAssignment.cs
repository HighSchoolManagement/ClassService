namespace ClassService.Domain.Entities
{
    public class ClassTeacherAssignment
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
        public bool IsHomeRoomTeacher { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }       // NULL = dang mo
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }   // cot DB: UpdatedDate
    }
}
