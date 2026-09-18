using AutoMapper;
using ClassService.Application.Classes.CreateClass;
using ClassService.Application.Classes.GetClassById;
using ClassService.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes
{
    public class ClassProfile: Profile
    {
        public ClassProfile()
        {
            CreateMap<ClassReadModel, GetClassByIdResponse>();
            CreateMap<ClassReadModel, CreateClassResponse>();
        }
    }
}
