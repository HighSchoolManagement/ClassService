namespace ClassService.Domain.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public int SchoolYearId { get; set; }        // FK kep (ClassId, SchoolYearId) -> Class
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }       // NULL = dang hoc
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }   // cot DB: UpdatedDate
        public Class Class { get; set; } = new Class();
    }
}
