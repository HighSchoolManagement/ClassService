using ClassService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Infrastructure.Configurations
{
    public class ClassSubjectConfiguration : IEntityTypeConfiguration<ClassSubject>
    {
        public void Configure(EntityTypeBuilder<ClassSubject> builder)
        {
            builder.ToTable("ClassSubject");
            builder.HasKey(c => c.Id);

            builder.HasOne(cb => cb.Class)
                .WithMany(c => c.ClassSubjects)
                .HasForeignKey(cb => cb.ClassId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => new { c.ClassId, c.SubjectId });
            

            throw new NotImplementedException();
        }
    }
}
