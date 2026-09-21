using AutoMapper;
using ClassService.Application.Models;
using ClassService.Domain.Entities;

namespace ClassService.Infrastructure.Mapping
{
    public class ClassMappingProfile : Profile
    {
        public ClassMappingProfile()
        {
            CreateMap<ClassCreateModel, Class>();

            CreateMap<Class, ClassReadModel>()
                .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
