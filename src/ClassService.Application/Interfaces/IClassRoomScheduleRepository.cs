using ClassService.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Interfaces
{
    public interface IClassRoomScheduleRepository
    {
        Task<List<ClassRoomScheduleReadModel>> GetClassRoomSchedule(int schoolId);
    }
}
