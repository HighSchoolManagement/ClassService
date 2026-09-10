using AutoMapper;
using ClassService.Application.Models;
using ClassService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Infrastructure.Mapping
{
    public class SchoolYearMappingProfile: Profile
    {
       public SchoolYearMappingProfile()
        {
            CreateMap<SchoolYear, SchoolYearReadModel>();
        }
    }
}
