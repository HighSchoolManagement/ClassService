namespace ClassService.Application.Models;

public class RoomReadModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SchoolId { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate {get;set;}
    public DateTime? ModifiedDate {get;set;}
}