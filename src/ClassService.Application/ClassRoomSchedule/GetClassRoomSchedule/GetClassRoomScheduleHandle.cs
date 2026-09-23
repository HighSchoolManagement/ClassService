using AutoMapper;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;
using ClassService.Application.Rooms;
using ClassService.Application.Schools.GetSchoolById;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.ClassRoomSchedule.GetClassRoomSchedule
{
    public class GetClassRoomScheduleHandle : IRequestHandler<GetClassRoomScheduleQuery, List<GetClassRoomScheduleResponse>>
    {
        private readonly ISchoolRepository schoolRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper mapper;
        private readonly IClassRoomScheduleRepository _classRoomScheduleRepository;
        public GetClassRoomScheduleHandle(ISchoolRepository schoolRepository, IMapper mapper
            , IClassRoomScheduleRepository classRoomScheduleRepository, IRoomRepository roomRepository)
        {
            this.schoolRepository = schoolRepository;
            this.mapper = mapper;
            _classRoomScheduleRepository = classRoomScheduleRepository;
            _roomRepository = roomRepository;
        }
        public async Task<List<GetClassRoomScheduleResponse>> Handle(GetClassRoomScheduleQuery request, CancellationToken cancellationToken)
        {
            if (request.RoomId <= 0 || request.SchoolId <= 0)
            {
                throw new ArgumentException("Id must be greater than 0");
            }

            if (request.To.HasValue)
            {
                if (request.From >= request.To)
                {
                    throw new ArgumentException("The end time must be greater than the start time");
                }
            }

            var start = request.From;
            var end = request.To ?? start.AddDays(1);
            var existingSchool = schoolRepository.GetSchoolReadModelByIdAsync(request.SchoolId);
            if (existingSchool == null)
            {
                throw new SchoolNotFoundException();
            }

            var existingRoomInSchool = _roomRepository.GetRoomBySchoolId(existingSchool.Id, request.RoomId);
            if (existingRoomInSchool == null)
            {
                throw new RoomNotFoundException();
            }
            // var response = await 
            //     _classRoomScheduleRepository.GetClassRoomSchedule(existingSchool.Id, existingRoomInSchool.Id, start,
            //         end,cancellationToken);
            // return mapper.Map<List<GetClassRoomScheduleResponse>>(response);
            return null;
        }
    }
}
