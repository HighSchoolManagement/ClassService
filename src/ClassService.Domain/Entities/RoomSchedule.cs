namespace ClassService.Domain.Entities;

public class RoomSchedule
{
    public int Id { get; set; }
    public int? ClassId { get; set; }
    public int RoomId { get; set; }
    public int PeriodId { get; set; }
    public BookingType Booking { get; set; }
    public DateOnly Date { get; set; }
    public DateTime CreatedDate {get;set;}
    public DateTime? ModifiedDate {get;set;}
    public string? Title { get; set; }
    public bool IsActive { get; set; }
    public Class? Class { get; set; }
    public Room Room { get; set; } = null!;
    public Period Period { get; set; } = null!;
}