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
    internal class ResourceStatusConfiguration : IEntityTypeConfiguration<ResourceProgramStatus>
    {
        public void Configure(EntityTypeBuilder<ResourceProgramStatus> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_status_id");

            builder.ToTable("resource_program_status", "rms");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CmsUser)
                .HasMaxLength(128)
                .HasColumnName("cms_user");
            builder.Property(e => e.Notes)
                .HasMaxLength(1024)
                .HasColumnName("notes");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");
            builder.Property(e => e.StatusTypeId).HasColumnName("status_type_id");
            builder.Property(e => e.Timestamp)
                .HasPrecision(3)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("timestamp");

            builder.HasOne(d => d.Resource).WithMany(p => p.ResourceStatuses)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_id_status");

            builder.HasOne(d => d.StatusType).WithMany(p => p.ResourceStatuses)
                .HasForeignKey(d => d.StatusTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_status_type_id");
        }
    }
}
