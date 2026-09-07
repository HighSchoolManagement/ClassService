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
            .HasColumnType("datetime2");
    }
}