using AutoMapper;
using ClassService.Application.Classes.GetClasses;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;
using ClassService.Application.Models;
using ClassService.Application.Schools.GetSchoolById;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Rooms.GetRooms
{
    public class GetRoomsHandle : IRequestHandler<GetRoomsQuery, PageResult<GetRoomsResponse>>
    {
        private readonly ISchoolRepository schoolRepository;
        private readonly IMapper mapper;
        private readonly IRoomRepository roomRepository;
        public GetRoomsHandle(ISchoolRepository schoolRepository, IRoomRepository roomRepository, IMapper mapper)
        {
            this.roomRepository = roomRepository;
            this.schoolRepository = schoolRepository;
            this.mapper = mapper;
        }
        public async Task<PageResult<GetRoomsResponse>> Handle(GetRoomsQuery request, CancellationToken cancellationToken = default)
        {
            if (request.SchoolId <= 0)
            {
                throw new ArgumentException("SchoolId must be greater than 0");
            }
            var existingSchool = await schoolRepository.GetSchoolReadModelByIdAsync(request.SchoolId);
            if (existingSchool == null)
            {
                throw new SchoolNotFoundException();
            }
            var (items, totalCount) =await roomRepository.GetPageRoom(request.PageNumber, request.PageSize, existingSchool.Id);
            return new PageResult<GetRoomsResponse>
            {
                Items = mapper.Map<List<GetRoomsResponse>>(items),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
