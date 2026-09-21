using System;
using System.Collections.Generic;
using System.Text;
using ClassService.Application.Models;

namespace ClassService.Application.Interfaces
{
    public interface IClassRepository
    {
        Task<(List<ClassReadModel> Items, int TotalCount)> GetListAsync(int? asOfId,int schoolYearId, int pageNumber, int pageSize);
        Task<int?> GetMaxClassIdAsync(int schoolYearId);
        Task<ClassReadModel> AddAsync(ClassCreateModel model);
        Task<ClassReadModel?> GetByIdAsync(int schoolYearId, int classId);
    }
}
