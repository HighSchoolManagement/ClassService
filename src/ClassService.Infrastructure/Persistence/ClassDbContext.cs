using ClassService.Domain.Entities;
using ClassService.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ClassService.Infrastructure.Persistence;

public class ClassDbContext: DbContext
{
    public ClassDbContext(DbContextOptions<ClassDbContext> options) : base(options)
    {
        
    }
    public DbSet<Class> Classes { get; set; }
    public DbSet<SchoolYear> SchoolYears { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<ClassTeacherAssignment> ClassTeacherAssignments { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<ClassRoomSchedule>  ClassRoomSchedules { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClassDbContext).Assembly);
    }
}