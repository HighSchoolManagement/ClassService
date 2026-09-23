using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClassService.Application.Interfaces;
using ClassService.Application.Models;
using ClassService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClassService.Infrastructure.Repositories;

public class RoomRepository:IRoomRepository
{
    private readonly IMapper _mapper;
    private readonly ClassDbContext _context;

    public RoomRepository(ClassDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<(List<RoomReadModel> Items, int TotalCount)> GetPageRoom(int pageNumber, int pageSize, int schoolId)
    {
        var query = _context.Rooms.Where(r => r.SchoolId == schoolId && r.IsActive).AsNoTracking();
        var totalCount =await query.CountAsync();
        var response = await query.OrderByDescending(r => r.CreatedDate).ThenBy(r => r.ModifiedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<RoomReadModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return (response, totalCount);
    }

    public async Task<RoomReadModel?> GetRoomBySchoolId(int schoolId, int roomId)
    {
        return await _context.Rooms
            .Where(r => r.SchoolId == schoolId && r.Id == roomId && r.IsActive)
            .ProjectTo<RoomReadModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}