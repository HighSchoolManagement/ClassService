using System;
using System.Collections.Generic;
using System.Text;
using ClassService.Application.Models;
using ClassService.Domain.Entities;

namespace ClassService.Application.Interfaces
{
    public interface IClassRepository
    {
        Task<(List<ClassReadModel> Items, int TotalCount)> GetListAsync(int schoolYearId, int schoolId, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<int?> GetMaxClassIdAsync(int schoolYearId);
        Task<ClassReadModel> AddAsync(ClassCreateModel model);
        Task<ClassReadModel?> GetByIdAsync(int schoolYearId, int schoolId , int classId);
        Task<Class?> GetByIdTrackedAsync(int schoolYearId, int schoolId, int classId);
        Task SaveChangesAsync();
    }
}
