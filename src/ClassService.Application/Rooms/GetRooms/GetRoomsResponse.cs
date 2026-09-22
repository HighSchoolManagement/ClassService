using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Rooms.GetRooms
{
    public class GetRoomsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SchoolId { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
    }
}
