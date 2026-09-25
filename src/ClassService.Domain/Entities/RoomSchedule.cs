namespace ClassService.Domain.Entities;

public class RoomSchedule
{
    public int Id { get; set; }
    public int ClassSubjectId { get; set; }
    public int RoomId { get; set; }
    public string Topic { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate {get;set;}
    public DateTime? ModifiedDate {get;set;}
    public ClassSubject ClassSubject { get; set; } = new ClassSubject();
    public Room Room { get; set; } = new Room();
}