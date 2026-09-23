using AutoMapper;
using ClassService.Application.Interfaces;
using ClassService.Application.Models;
using ClassService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Infrastructure.Repositories
{
    public class ClassRoomScheduleRepository : IClassRoomScheduleRepository
    {
        private readonly ClassDbContext _context;
        private readonly IMapper _mapper;
        public ClassRoomScheduleRepository(ClassDbContext context, ISchoolRepository schoolRepository, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ClassRoomScheduleReadModel>> GetClassRoomSchedule(int roomId)
        {
            var response =await _context.ClassRoomSchedules.Where(c => c.RoomId == roomId).ToListAsync();
            return _mapper.Map<List<ClassRoomScheduleReadModel>>(response);
        }
    }
}
