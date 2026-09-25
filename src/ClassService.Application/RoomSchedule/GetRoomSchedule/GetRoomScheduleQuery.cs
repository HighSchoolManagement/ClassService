using ClassService.Application.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.ClassRoomSchedule.GetClassRoomSchedule
{
    public class GetRoomScheduleQuery: IRequest<GetRoomScheduleResponse>
    {
        public int SchoolId { get; set; }
        public int RoomId { get; set; }
        public DateOnly From { get; set; }
        public DateOnly? To { get; set; }
    }
}
