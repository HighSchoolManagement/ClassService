using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClassService.Application.Interfaces;
using ClassService.Application.Models;
using ClassService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Infrastructure.Repositories
{
    public class PeriodRepository : IPeriodRepository
    {
        private readonly ClassDbContext _context;
        private readonly IMapper _mapper;
        public PeriodRepository(ClassDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<PeriodReadModel>> GetListPeriodAsync(int schoolYearId, CancellationToken cancellationToken)
        {
            return await _context.Periods
                .AsNoTracking()
                .Where(p => p.SchoolYearId == schoolYearId && p.IsActive)
                .ProjectTo<PeriodReadModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        }
    }
}
