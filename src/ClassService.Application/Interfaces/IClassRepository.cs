using System;
using System.Collections.Generic;
using System.Text;
using ClassService.Application.Models;

namespace ClassService.Application.Interfaces
{
    public interface IClassRepository
    {
        Task<(List<ClassReadModel> Items, int TotalCount)> GetListAsync(int? asOfId, int pageNumber, int pageSize);
        Task<int?> GetMaxClassIdAsync(int schoolYear);
        Task<ClassReadModel> AddAsync(ClassCreateModel model);
    }
}
