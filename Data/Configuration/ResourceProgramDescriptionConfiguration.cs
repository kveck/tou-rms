using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MigrateTOUData.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MigrateTOUData.Data.Configuration
{
    internal class ResourceProgramDescriptionConfiguration : IEntityTypeConfiguration<ResourceProgramDescription>
    {
        public void Configure(EntityTypeBuilder<ResourceProgramDescription> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_program_description");

            builder.ToTable("resource_program_description", "rms");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Description).HasColumnName("description");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");

            builder.HasOne(d => d.Resource).WithMany(p => p.Descriptions)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_id_description");
        }
    }
}
