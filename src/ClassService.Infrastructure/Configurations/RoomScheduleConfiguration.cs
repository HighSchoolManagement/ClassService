using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassService.Infrastructure.Configurations
{
    internal class RoomScheduleConfiguration : IEntityTypeConfiguration<RoomSchedule>
    {
        public void Configure(EntityTypeBuilder<RoomSchedule> builder)
        {
            // Gia tri BookingType trong CHECK: Class = 1 (xem enum BookingType). Doi enum thi sua CHECK.
            builder.ToTable("RoomSchedule", t =>
            {
                t.HasCheckConstraint("CK_RoomSchedule_Lesson_ClassSubject",
                    "[BookingType] <> 1 OR [ClassSubjectId] IS NOT NULL");
                t.HasCheckConstraint("CK_RoomSchedule_NonLesson_Title",
                    "[BookingType] = 1 OR [Title] IS NOT NULL");
                t.HasCheckConstraint("CK_RoomSchedule_NonLesson_NoTopic",
                    "[BookingType] = 1 OR [Topic] IS NULL");
            });
            builder.HasKey(e => e.Id);

            builder.Property(x => x.BookingType)
                .HasConversion<byte>()
                .HasColumnType("tinyint")
                .IsRequired();
            builder.Property(x => x.Title).HasMaxLength(200);
            builder.Property(x => x.Topic).HasMaxLength(200);

            builder.HasOne(x => x.Room).WithMany(r => r.RoomSchedules).HasForeignKey(x => x.RoomId);
            builder.HasOne(x => x.Period).WithMany(p => p.RoomSchedules).HasForeignKey(x => x.PeriodId);
            builder.HasOne(x => x.ClassSubject)
                .WithMany(cs => cs.RoomSchedules)
                .HasForeignKey(x => x.ClassSubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.RowVersion).IsRowVersion();

            // Phong khong bi chiem doi (o dong cua lop da xoa van IsActive = 1 nen van giu o).
            builder.HasIndex(x => new { x.RoomId, x.Date, x.PeriodId })
                .IsUnique()
                .HasFilter("[IsActive] = 1");

            // "Mot lop khong o hai phong cung tiet" khong index duoc (phai qua ClassSubject) -> kiem tra bang code.

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime2");

            builder.Property(e => e.ModifiedDate)
                .HasColumnName("UpdatedDate")
                .HasColumnType("datetime2");
        }
    }
}
