namespace ClassService.Domain.Entities;

public class RoomSchedule
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public int PeriodId { get; set; }
    public DateOnly Date { get; set; }
    public BookingType BookingType { get; set; }
    public int? ClassSubjectId { get; set; }         // bat buoc khi BookingType = Class
    public string? Topic { get; set; }               // chi cho buoi hoc
    public string? Title { get; set; }               // bat buoc khi Meeting / Reserved
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }      // cot DB: UpdatedDate
    public byte[] RowVersion { get; set; } = null!;
    public Room Room { get; set; } = null!;
    public Period Period { get; set; } = null!;
    public ClassSubject? ClassSubject { get; set; }
}
