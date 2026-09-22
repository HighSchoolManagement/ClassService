namespace ClassService.Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SchoolId { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate {get;set;}
    public DateTime? ModifiedDate {get;set;}
    public ICollection<ClassRoomSchedule> ClassRoomSchedules { get; set; } = new List<ClassRoomSchedule>();

}