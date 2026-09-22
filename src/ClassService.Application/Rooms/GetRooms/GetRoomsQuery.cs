using ClassService.Application.Common.Mediator;
using ClassService.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Rooms.GetRooms
{
    public class GetRoomsQuery : IRequest<PageResult<GetRoomsResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int SchoolId { get; set; }
    }
}
