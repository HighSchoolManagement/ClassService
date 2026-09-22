namespace ClassService.Domain.Entities;

public class ClassRoomSchedule
{
    public int Id { get; set; }
    public int ClassId { get; set; }
    public int RoomId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime? CreatedDate {get;set;}
    public DateTime? ModifiedDate {get;set;}
    public Class Class { get; set; } = new Class();
    public Room Room { get; set; } = new Room();
}