using ClassService.Application.Common.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using ClassService.Application.Models;
using ClassService.Domain.Entities;


namespace ClassService.Application.Interfaces
{
    public interface IClassRepository
    {
        Task<(List<ClassReadModel> Items, int TotalCount)> GetListAsync(IEnumerable<ISpecification<Class>> specs,  int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<ClassReadModel> AddAsync(ClassCreateModel model, CancellationToken cancellationToken = default);
        Task<ClassReadModel?> GetByIdAsync(ISpecification<Class> spec, CancellationToken cancellationToken = default);
        Task<Class?> GetByIdTrackedAsync(ISpecification<Class> spec, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
