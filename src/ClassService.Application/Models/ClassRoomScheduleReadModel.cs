using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Models
{
    public class ClassRoomScheduleReadModel
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
