using AutoMapper;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;
using ClassService.Application.Models;
using ClassService.Application.Rooms;
using ClassService.Application.Schools.GetSchoolById;
using ClassService.Application.SchoolYears.GetSchoolYearById;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.ClassRoomSchedule.GetClassRoomSchedule
{
    public class GetRoomScheduleHandle : IRequestHandler<GetRoomScheduleQuery, GetRoomScheduleResponse>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISchoolYearRepository _schoolYearRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IPeriodRepository _periodRepository;
        private readonly IMapper mapper;
        private readonly IRoomScheduleRepository _roomScheduleRepository;
        private const int MaxRangeDays = 31;

        public GetRoomScheduleHandle(ISchoolRepository schoolRepository, IMapper mapper
            , IRoomScheduleRepository roomScheduleRepository, IRoomRepository roomRepository, ISchoolYearRepository schoolYearRepository, IPeriodRepository periodRepository)
        {
            _schoolRepository = schoolRepository;
            this.mapper = mapper;
            _roomScheduleRepository = roomScheduleRepository;
            _roomRepository = roomRepository;
            _schoolYearRepository = schoolYearRepository;
            _periodRepository = periodRepository;
        }
        public async Task<GetRoomScheduleResponse> Handle(GetRoomScheduleQuery request, CancellationToken cancellationToken)
        {
            if (request.RoomId <= 0 || request.SchoolId <= 0)
            {
                throw new ArgumentException("Id must be greater than 0");
            }
            var from = request.From;
            var to = request.To ?? request.From;          

            if (from > to)
            {
                throw new ArgumentException("'To' must not be earlier than 'From'");
            }
            if (to.DayNumber - from.DayNumber + 1 > MaxRangeDays)
            {
                throw new ArgumentException($"Date range must not exceed {MaxRangeDays} days");

            }
            var endExclusive = to.AddDays(1);

            var school = await _schoolRepository.GetSchoolReadModelByIdAsync(request.SchoolId);
            if (school is null)
                throw new SchoolNotFoundException();

            var room = await _roomRepository.GetRoomBySchoolIdAsync(school.Id, request.RoomId, cancellationToken);
            if (room is null)
                throw new RoomNotFoundException();

            var schoolYear = await _schoolYearRepository.GetSchoolYearReadModelBySchoolIdAndDateAsync(
            school.Id, from, cancellationToken);
            if (schoolYear is null)
                throw new SchoolYearNotFoundException();


            var periods = await _periodRepository.GetListPeriodAsync(schoolYear.Id, cancellationToken);

            var items = await _roomScheduleRepository.GetRoomScheduleAsync(
             school.Id, room.Id, from, endExclusive, cancellationToken);

            return new GetRoomScheduleResponse
            {
                Room = new GetRoomScheduleResponse.RoomInfo
                {
                    Id = room.Id,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    IsActive = room.IsActive
                },
                SchoolYear = new GetRoomScheduleResponse.SchoolYearInfo
                {
                    Id = schoolYear.Id,
                    Name = schoolYear.Name,
                    StartDate = schoolYear.StartDate,
                    EndDate = schoolYear.EndDate
                },
                Periods = periods
                .OrderBy(p => p.Number)
                .Select(p => new GetRoomScheduleResponse.PeriodInfo
                {
                    Id = p.Id,
                    Number = p.Number,
                    StartTime = p.StartTime,
                    EndTime = p.EndTime
                })
                .ToList(),
                Items = items
                .Select(i => new GetRoomScheduleResponse.ItemInfo
                {
                    Id = i.Id,
                    Date = i.Date,
                    PeriodId = i.PeriodId,
                    Type = i.Type.ToString(),
                    Title = i.Title,
                    ClassId = i.ClassId,
                    ClassName = i.ClassName,
                    StudentCount = i.StudentCount
                })
                .ToList(),
                Summary = BuildSummary(from, to, periods.Count, items)
            };
        }

        private static GetRoomScheduleResponse.SummaryInfo BuildSummary(
        DateOnly from, DateOnly to, int periodCount, IReadOnlyCollection<RoomScheduleReadModel> items)
        {
            var schoolDays = 0;
            for (var d = from; d <= to; d = d.AddDays(1))
            {
                if (IsSchoolDay(d)) schoolDays++;
            }

            var booked = items.Count(i => IsSchoolDay(i.Date));
            var classCount = items
                .Where(i => i.ClassId.HasValue)
                .Select(i => i.ClassId!.Value)
                .Distinct()
                .Count();

            return new GetRoomScheduleResponse.SummaryInfo
            {
                ClassCount = classCount,
                BookedPeriodCount = booked,
                FreePeriodCount = Math.Max(0, schoolDays * periodCount - booked)
            };
        }
        private static bool IsSchoolDay(DateOnly d) =>
          d.DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday);
    }
}
