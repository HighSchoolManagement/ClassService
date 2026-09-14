using ClassService.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Interfaces
{
    public interface ISchoolYearRepository
    {
        Task<SchoolYearReadModel?> GetSchoolYearReadModelByIdAsync(int id);
        Task<SchoolYearReadModel?> GetSchoolYearReadModelByNameAsync(string name);
        Task<SchoolYearReadModel> AddAsync(SchoolYearCreateModel model);
    }
}
