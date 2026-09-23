using AutoMapper;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.ClassRoomSchedule.GetClassRoomSchedule
{
    public class GetClassRoomScheduleHandle : IRequestHandler<GetClassRoomScheduleQuery, List<GetClassRoomScheduleResponse>>
    {
        private readonly ISchoolRepository schoolRepository;
        private readonly IMapper mapper;
        public GetClassRoomScheduleHandle(ISchoolRepository schoolRepository, IMapper mapper)
        {
            this.schoolRepository = schoolRepository;
            this.mapper = mapper;
        }
        public Task<List<GetClassRoomScheduleResponse>> Handle(GetClassRoomScheduleQuery request, CancellationToken cancellationToken = default)
        {
            if (request.RoomId <= 0 || request.SchoolId <= 0)
            {
                throw new ArgumentException("Id must be greater than 0");
            }
            if (request.From >= request.To)
            {
                throw new ArgumentException("The end time must be greater than the start time");
            }
            throw new NotImplementedException();
        }
    }
}
