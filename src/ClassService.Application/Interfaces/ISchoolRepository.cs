using ClassService.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Interfaces
{
    public interface ISchoolRepository
    {
        Task<SchoolReadModel?> GetSchoolReadModelByIdAsync(int id);
    }
}
