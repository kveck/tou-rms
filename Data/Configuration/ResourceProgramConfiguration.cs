using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MigrateTOUData.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateTOUData.Data.Configuration
{
    internal class ResourceProgramConfiguration : IEntityTypeConfiguration<ResourceProgram>
    {
        public void Configure(EntityTypeBuilder<ResourceProgram> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_program");

            builder.ToTable("resource_program", "rms");

            builder.HasIndex(e => e.DetailId, "uq_detail_id").IsUnique();

            builder.HasIndex(e => e.OrgId, "uq_org_id").IsUnique();

            builder.HasIndex(e => e.ResourceCode, "uq_resource_code").IsUnique();

            builder.HasIndex(e => e.StatusId, "uq_status_id_resource").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.DetailId).HasColumnName("detail_id");
            builder.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
            builder.Property(e => e.OrgId).HasColumnName("org_id");
            builder.Property(e => e.ResourceCode)
                .HasComputedColumnSql("((10000)+[id])", false)
                .HasColumnName("resource_code");
            builder.Property(e => e.ResourceUrl)
                .HasMaxLength(3000)
                .HasColumnName("resource_url");
            builder.Property(e => e.StatusId).HasColumnName("status_id");
            builder.Property(e => e.TimestampCreated)
                .HasPrecision(3)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("timestamp_created");

            builder.HasOne(d => d.Detail).WithOne(p => p.Resource)
                .HasForeignKey<ResourceProgram>(d => d.DetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_detail_id");

            builder.HasOne(d => d.Org).WithMany(p => p.ResourcePrograms)
                .HasForeignKey(d => d.OrgId)
                .HasConstraintName("fk_resource_organization_id");

            // resource program stores a single status, as the latest status
            builder.HasOne(d => d.Status).WithOne(p => p.Resource)
                .HasForeignKey<ResourceProgram>(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_status_id");
        }
    }
}
