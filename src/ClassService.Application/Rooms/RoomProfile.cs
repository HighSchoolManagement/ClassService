using AutoMapper;
using ClassService.Application.Classes.GetClassById;
using ClassService.Application.Models;
using ClassService.Application.Rooms.GetRooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Rooms
{
    public class RoomProfile : Profile
    {
        public RoomProfile() {
            CreateMap<RoomReadModel, GetRoomsResponse>();
        }
    }
}
