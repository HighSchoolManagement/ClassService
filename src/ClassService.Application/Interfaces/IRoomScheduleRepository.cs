using ClassService.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Interfaces
{
    public interface IRoomScheduleRepository
    {
        Task<List<RoomScheduleReadModel>> GetRoomScheduleAsync(
        int schoolId, int roomId, DateOnly from, DateOnly toExclusive, CancellationToken cancellationToken = default);
    }
}
