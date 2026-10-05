namespace ClassService.Domain.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }   // cot DB: UpdatedDate
    }
}
