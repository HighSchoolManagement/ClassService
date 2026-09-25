namespace ClassService.Domain.Entities
{
    public class Period
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public int SchoolYearId { get; set; }
        public int Number { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsActive { get; set; }
        public SchoolYear SchoolYear { get; set; } = null!;
        public ICollection<RoomSchedule> RoomSchedules { get; set; } = null!;
    }
}