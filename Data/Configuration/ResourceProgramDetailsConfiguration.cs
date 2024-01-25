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
    internal class ResourceProgramDetailsConfiguration : IEntityTypeConfiguration<ResourceProgramDetail>
    {
        public void Configure(EntityTypeBuilder<ResourceProgramDetail> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_program_detail");

            builder.ToTable("resource_program_detail", "rms");

            builder.HasIndex(e => e.ProcessTimeId, "uq_process_time_id_detail");

            builder.HasIndex(e => e.DescriptionId, "uq_resource_description_id_detail").IsUnique();

            builder.HasIndex(e => e.ResourceId, "uq_resource_id_detail").IsUnique();

            builder.HasIndex(e => e.InternalNotesId, "uq_resource_notes_id_detail").IsUnique();

            builder.HasIndex(e => e.ProcessStepsId, "uq_resource_steps_id_detail").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Cost)
                .HasMaxLength(128)
                .HasColumnName("cost");
            builder.Property(e => e.CustomerServiceRating).HasColumnName("customer_service_rating");
            builder.Property(e => e.DescriptionId).HasColumnName("description_id");
            builder.Property(e => e.InternalNotesId).HasColumnName("internal_notes_id");
            builder.Property(e => e.ObtainabilityRating).HasColumnName("obtainability_rating");
            builder.Property(e => e.ProcessStepsId).HasColumnName("process_steps_id");
            builder.Property(e => e.ProcessTimeId).HasColumnName("process_time_id");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");

            builder.HasOne(d => d.Description).WithOne(p => p.ResourceDetail)
                .HasForeignKey<ResourceProgramDetail>(d => d.DescriptionId)
                .HasConstraintName("fk_resource_description_id_detail");

            builder.HasOne(d => d.InternalNotes).WithOne(p => p.ResourceDetail)
                .HasForeignKey<ResourceProgramDetail>(d => d.InternalNotesId)
                .HasConstraintName("fk_internal_notes_id_detail");

            builder.HasOne(d => d.ProcessSteps).WithOne(p => p.ResourceDetail)
                .HasForeignKey<ResourceProgramDetail>(d => d.ProcessStepsId)
                .HasConstraintName("fk_resource_steps_id_detail");

            builder.HasOne(d => d.ProcessTime).WithMany(p => p.ResourceDetails)
               .HasForeignKey(d => d.ProcessTimeId)
               .HasConstraintName("fk_resource_process_time_id");

            builder.HasOne(d => d.Resource).WithOne(p => p.ResourceProgramDetail)
                .HasForeignKey<ResourceProgramDetail>(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_id_detail");
        }
    }
}
