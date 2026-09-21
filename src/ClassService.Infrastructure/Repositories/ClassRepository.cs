using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClassService.Application;
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
        public async Task<ClassReadModel> AddAsync(ClassCreateModel model)
        {
            var classEntity = _mapper.Map<Class>(model);
            _context.Add(classEntity);
            try{
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException)
                {
                    if (sqlException.Number == SqlServerErrorCodes.UniqueIndexViolation ||
                        sqlException.Number == SqlServerErrorCodes.UniqueConstraintViolation)
                    {
                        throw new DuplicateClassNameException();
                    }
                }
                throw;
            }
            return _mapper.Map<ClassReadModel>(classEntity);
        }

        public async Task<ClassReadModel?> GetByIdAsync(int schoolYearId, int classId)
        {
            var classEntity = await _context.Classes
                .Where(c => c.SchoolYearId == schoolYearId && c.Id == classId)
                .ProjectTo<ClassReadModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return classEntity;
        }

        public async Task<Class?> GetByIdTrackedAsync(int schoolYearId, int classId)
        {
            return await _context.Classes
               .Where(c => c.SchoolYearId == schoolYearId && c.Id == classId)
               .FirstOrDefaultAsync();
  
        }

        public async Task<(List<ClassReadModel> Items, int TotalCount)> GetListAsync(int? asOfId,int schoolYearId, int pageNumber, int pageSize)
        {
            
            var query = _context.Classes.AsQueryable();
            if (asOfId.HasValue)
            {
                query = query.Where(c =>  c.Id <= asOfId && c.SchoolYearId == schoolYearId).AsQueryable();
            }else
            {
                query = query.Where(c => c.SchoolYearId == schoolYearId).AsQueryable();
            }
            var totalCount = await query.CountAsync();

            var classEntities = await query
                .OrderByDescending(c => c.CreatedDate).ThenByDescending(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ClassReadModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return (classEntities, totalCount);
        }

        public async Task<int?> GetMaxClassIdAsync(int schoolYearId)
        {
            return await _context.Classes.Where(c => c.SchoolYearId == schoolYearId).Select(c => (int?)c.Id).MaxAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
