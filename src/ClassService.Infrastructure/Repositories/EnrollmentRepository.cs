using ClassService.Application.Interfaces;
using ClassService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClassService.Infrastructure.Repositories
{
    public class EnrollmentRepository: IEnrollmentRepository
    {
        private readonly ClassDbContext _context;

        public EnrollmentRepository(ClassDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetStudentEnrollmentTotal(int classId)
        {
            return await _context.Enrollments.AsNoTracking()
                .Where(e => e.ClassId == classId).CountAsync();
        }
    }
}