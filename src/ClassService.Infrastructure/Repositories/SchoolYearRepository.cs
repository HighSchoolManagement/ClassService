using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClassService.Application.Classes.CreateClass;
using ClassService.Application.Interfaces;
using ClassService.Application.Models;
using ClassService.Application.SchoolYears.CreateSchoolYear;
using ClassService.Application.SchoolYears.GetSchoolYearById;
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
    public class SchoolYearRepository : ISchoolYearRepository
    {
        private readonly ClassDbContext _context;
        private readonly IMapper _mapper;
        public SchoolYearRepository (ClassDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<SchoolYearReadModel?> GetSchoolYearReadModelByIdAsync(int id)
        {

            var schoolYear =await _context.SchoolYears.AsNoTracking().Where(sy => sy.Id == id && sy.IsActive)
                .ProjectTo<SchoolYearReadModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return schoolYear;
        }

        public async Task<SchoolYearReadModel?> GetSchoolYearReadModelByNameAsync(string name)
        {
            var schoolYearName = name.Trim();
            var schoolYear = await _context.SchoolYears.AsNoTracking().Where(sy => sy.Name == schoolYearName && sy.IsActive)
               .ProjectTo<SchoolYearReadModel>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync();

            return schoolYear;
        }

        public async Task<SchoolYearReadModel> AddAsync(SchoolYearCreateModel model)
        {
            var shoolYearEntity = _mapper.Map<SchoolYear>(model);
            _context.Add(shoolYearEntity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException)
                {
                    if (sqlException.Number == SqlServerErrorCodes.UniqueIndexViolation ||
                        sqlException.Number == SqlServerErrorCodes.UniqueConstraintViolation)
                    {
                        throw new DuplicateSchoolYearNameException();
                    }
                }
                throw;
            }
            return _mapper.Map<SchoolYearReadModel>(shoolYearEntity);
        }

        public async Task<SchoolYearReadModel?> GetSchoolYearReadModelBySchoolIdAndDateAsync(int schoolId, DateOnly date, CancellationToken cancellationToken = default)
        {
            var schoolYear = await _context.SchoolYears.AsNoTracking().Where(sy => sy.SchoolId == schoolId && sy.StartDate <= date && date <= sy.EndDate && sy.IsActive)
                         .ProjectTo<SchoolYearReadModel>(_mapper.ConfigurationProvider)
                         .FirstOrDefaultAsync(cancellationToken);
            return schoolYear;
        }
    }
}
