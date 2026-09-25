using AutoMapper;
using ClassService.Application.Models;
using ClassService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Infrastructure.Mapping
{
    public class PeriodProfile : Profile
    {
        public PeriodProfile()
        {
            CreateMap<Period, PeriodReadModel>();
        }
    }
}
