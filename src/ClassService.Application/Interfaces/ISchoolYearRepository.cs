using ClassService.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Interfaces
{
    public interface ISchoolYearRepository
    {
        Task<SchoolYearReadModel?> GetSchoolYearReadModelByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<SchoolYearReadModel?> GetSchoolYearReadModelBySchoolIdAndDateAsync(int schoolId, DateOnly date, CancellationToken cancellationToken = default);
        Task<SchoolYearReadModel?> GetSchoolYearReadModelByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<SchoolYearReadModel> AddAsync(SchoolYearCreateModel model, CancellationToken cancellationToken = default);
    }
}
