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
            builder.HasKey(e => e.Id).HasName("pk_resource_program_details");

            builder.ToTable("resource_program_details", "rms");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Cost)
                .HasMaxLength(128)
                .HasColumnName("cost");
            builder.Property(e => e.CustomerServiceRating).HasColumnName("customer_service_rating");
            builder.Property(e => e.ObtainabilityRating).HasColumnName("obtainability_rating");
            builder.Property(e => e.ProcessTimeId).HasColumnName("process_time_id");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");

            builder.HasOne(d => d.ProcessTime).WithMany(p => p.ResourceProgramDetails)
                .HasForeignKey(d => d.ProcessTimeId)
                .HasConstraintName("fk_resource_process_time_id");

            builder.HasOne(d => d.Resource).WithMany(p => p.ProgramDetails)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_id_details");
        }
    }
}
