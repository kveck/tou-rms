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
    internal class ResourceProgramStepConfiguration : IEntityTypeConfiguration<ResourceProgramStep>
    {
        public void Configure(EntityTypeBuilder<ResourceProgramStep> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_program_steps");

            builder.ToTable("resource_program_steps", "rms");

            builder.HasIndex(e => e.ResourceDetailId, "uq_resource_id_steps").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ProcessSteps).HasColumnName("process_steps");
            builder.Property(e => e.ResourceDetailId).HasColumnName("resource_detail_id");

            builder.HasOne(d => d.ResourceDetail).WithOne(p => p.ProcessSteps)
                .HasForeignKey<ResourceProgramStep>(d => d.ResourceDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_id_steps");
        }
    }
}
