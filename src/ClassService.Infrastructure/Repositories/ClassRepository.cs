using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClassService.Application;
using ClassService.Application.Classes;
using ClassService.Application.Classes.CreateClass;
using ClassService.Application.Interfaces;
using ClassService.Application.Models;
using ClassService.Domain.Entities;
using ClassService.Infrastructure.Common;
using ClassService.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Infrastructure.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly IMapper _mapper;
        private readonly ClassDbContext _context;
        public ClassRepository(IMapper mapper, ClassDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ClassReadModel> AddAsync(ClassCreateModel model, CancellationToken cancellationToken = default)
        {
            var classEntity = _mapper.Map<Class>(model);
            _context.Add(classEntity);
            await SaveChangesAsync(cancellationToken);
            return _mapper.Map<ClassReadModel>(classEntity);
        }

        public async Task<ClassReadModel?> GetByIdAsync(int schoolYearId, int schoolId, int classId)
        {
            var classEntity = await _context.Classes
                .AsNoTracking()
                .Where(c => c.SchoolYearId == schoolYearId && c.SchoolId == schoolId && c.Id == classId)
                .ProjectTo<ClassReadModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return classEntity;
        }

        public async Task<Class?> GetByIdTrackedAsync(int schoolYearId,int schoolId, int classId, CancellationToken cancellationToken)
        {
            return await _context.Classes
               .Where(c => c.SchoolYearId == schoolYearId && c.SchoolId == schoolId && c.Id == classId && c.IsActive)
               .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<(List<ClassReadModel> Items, int TotalCount)> GetListAsync(int schoolYearId, int schoolId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var  query = _context.Classes.AsNoTracking().Where(c => c.SchoolYearId == schoolYearId && c.SchoolId == schoolId && c.IsActive).AsQueryable();
            var totalCount = await query.CountAsync(cancellationToken);

            var classEntities = await query
                .OrderByDescending(c => c.CreatedDate).ThenByDescending(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ClassReadModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return (classEntities, totalCount);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new RowVersionConflitException();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException s &&
                                               (s.Number == SqlServerErrorCodes.UniqueIndexViolation ||
                                                s.Number == SqlServerErrorCodes.UniqueConstraintViolation))
            {
                throw new DuplicateClassNameException();
            }
           
        }
    }
}
