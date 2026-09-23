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
    public class RoomScheduleRepository : IClassRoomScheduleRepository
    {
        private readonly ClassDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISchoolRepository _schoolRepository;
        public RoomScheduleRepository(ClassDbContext context, ISchoolRepository schoolRepository, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ClassRoomScheduleReadModel>> GetClassRoomSchedule(int schoolId, int roomId, DateTime from, DateTime to, CancellationToken token)
        {
            
            // var response =await _context
            //     .ClassRoomSchedules
            //     .Where(c => c.Room.SchoolId == schoolId &&  c.RoomId == roomId && c.StartTime == from && c.EndTime == to) 
            //     .ToListAsync();
            // return _mapper.Map<List<ClassRoomScheduleReadModel>>(response);
            throw new Exception();
        }
    }
}
