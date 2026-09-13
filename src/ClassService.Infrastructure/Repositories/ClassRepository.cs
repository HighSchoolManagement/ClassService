using ClassService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ClassService.Application.Classes.CreateClass;
using ClassService.Application.Models;
using ClassService.Domain.Entities;
using ClassService.Infrastructure.Common;
using ClassService.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
    }
}
