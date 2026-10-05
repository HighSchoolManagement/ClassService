using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClassService.Domain.Entities;

namespace ClassService.Infrastructure.Configurations;

public class ClassConfiguration : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("Class");
        builder.HasKey(c => c.Id);

        // Khoa phu (Id, SchoolYearId) la dich cua FK kep tu Enrollment.
        builder.HasAlternateKey(c => new { c.Id, c.SchoolYearId });

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Capacity)
            .HasColumnType("int");

        builder.HasOne(c => c.SchoolYear)
            .WithMany(sy => sy.Classes)
            .HasForeignKey(c => c.SchoolYearId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(c => c.CreatedDate)
            .HasColumnType("datetime2");

        builder.Property(c => c.ModifiedDate)
            .HasColumnName("UpdatedDate")
            .HasColumnType("datetime2");

        builder.Property(c => c.RowVersion)
            .IsRowVersion();

        // Ten lop duy nhat trong pham vi mot nam hoc, chi tinh lop dang active
        // (lop da xoa IsActive = 0 khong chiem ten).
        builder.HasIndex(c => new { c.SchoolYearId, c.Name })
            .IsUnique()
            .HasFilter("[IsActive] = 1");
    }
}
