using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Infrastructure.Configurations
{
    public class SchoolYearConfiguration : IEntityTypeConfiguration<SchoolYear>
    {
        public void Configure(EntityTypeBuilder<SchoolYear> builder)
        {
            builder.ToTable("SchoolYear", s => s.HasCheckConstraint("CK_SchoolYear_DateRange", "[EndDate] > [StartDate]"));
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
            .HasMaxLength(100)
            .IsRequired();

            builder.HasIndex(s => s.Name)
                .IsUnique();

            builder.Property(s => s.IsActive)
                .HasColumnType("BIT")
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(s => s.StartDate)
       .HasColumnType("datetime2");

            builder.Property(s => s.EndDate)
                .HasColumnType("datetime2");

            builder.Property(s => s.CreatedDate)
           .HasColumnType("datetime2");

            builder.Property(s => s.ModifiedDate)
                .HasColumnType("datetime2");
        }
    }
}
