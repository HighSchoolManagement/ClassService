using ClassService.Application.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.ClassRoomSchedule.GetClassRoomSchedule
{
    public class GetClassRoomScheduleQuery: IRequest<List<GetClassRoomScheduleResponse>>
    {
        public int SchoolId { get; set; }
        public int RoomId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
