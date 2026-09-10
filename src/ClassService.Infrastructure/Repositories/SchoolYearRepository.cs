using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClassService.Application.Interfaces;
using ClassService.Application.Models;
using ClassService.Application.SchoolYears.GetSchoolYearById;
using ClassService.Infrastructure.Persistence;
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

            var schoolYear =await _context.SchoolYears.Where(sy => sy.Id == id && sy.IsActive)
                .ProjectTo<SchoolYearReadModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return schoolYear;
        }
    }
}
