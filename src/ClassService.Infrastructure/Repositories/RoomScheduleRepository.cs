using AutoMapper;
using ClassService.Application.Interfaces;
using ClassService.Application.Models;
using ClassService.Domain.Entities;
using ClassService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Infrastructure.Repositories
{
    public class RoomScheduleRepository : IRoomScheduleRepository
    {
        private readonly ClassDbContext _context;

        public RoomScheduleRepository(ClassDbContext context, ISchoolRepository schoolRepository)
        {
            _context = context;
        }
        public async Task<List<RoomScheduleReadModel>> GetRoomScheduleAsync(
        int schoolId, int roomId, DateOnly from, DateOnly toExclusive, CancellationToken cancellationToken = default)
        {
            return await _context.RoomSchedules
                .AsNoTracking()
                .Where(s => s.RoomId == roomId
                         && s.Room.SchoolId == schoolId          // room must belong to the school
                         && s.Date >= from && s.Date < toExclusive
                         && s.IsActive)
                // add "&& s.IsActive" if RoomSchedule gets an IsActive flag (recommended: needed for the unique filtered index)
                .OrderBy(s => s.Date)
                .ThenBy(s => s.Period.Number)
                .Select(s => new RoomScheduleReadModel
                {
                    Id = s.Id,
                    Date = s.Date,
                    PeriodId = s.PeriodId,
                    Type = s.Booking,
                    Title = s.Title,
                    ClassId = s.ClassId,
                    ClassName = s.Class != null ? s.Class.Name : null,       // Meeting/Reserved have no class
                    StudentCount = s.Class == null
                        ? (int?)null
                        // only enrollments valid ON THE DAY of the booking
                        : s.Class.Enrollments.Count(e => e.StartDate <= s.Date && (e.EndDate == null || e.EndDate >= s.Date))
                })
                .ToListAsync(cancellationToken);
        }
    }
}
