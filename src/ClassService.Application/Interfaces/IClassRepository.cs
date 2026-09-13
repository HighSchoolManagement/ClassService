using System;
using System.Collections.Generic;
using System.Text;
using ClassService.Application.Models;

namespace ClassService.Application.Interfaces
{
    public interface IClassRepository
    {
        Task<ClassReadModel> AddAsync(ClassCreateModel model);
    }
}
