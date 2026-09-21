using ClassService.Application.Models;

namespace ClassService.Application.Interfaces;

public interface IRoomRepository
{
    Task<(List<RoomReadModel>Items, int TotalCount)> GetPageRoom(int pageNumber, int pageSize, int schoolId);
}