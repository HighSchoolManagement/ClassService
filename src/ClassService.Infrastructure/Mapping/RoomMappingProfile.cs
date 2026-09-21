using AutoMapper;
using ClassService.Application.Models;
using ClassService.Domain.Entities;

namespace ClassService.Infrastructure.Mapping;

public class RoomMappingProfile : Profile
{
    public RoomMappingProfile()
    {
        CreateMap<Room, RoomReadModel>();
    }
}