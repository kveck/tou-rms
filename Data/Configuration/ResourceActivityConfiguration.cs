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
    internal class ResourceActivityConfiguration : IEntityTypeConfiguration<ResourceActivity>
    {
        public void Configure(EntityTypeBuilder<ResourceActivity> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_resource_activity");

            builder.ToTable("resource_activity", "rms");

            builder.HasIndex(e => e.ActivityDetailId, "uq_activity_detail_id").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ActivityDetailId).HasColumnName("activity_detail_id");
            builder.Property(e => e.ActivityTypeId).HasColumnName("activity_type_id");
            builder.Property(e => e.CmsUser)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("cms_user");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");
            builder.Property(e => e.Timestamp)
                .HasPrecision(3)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("timestamp");

            builder.HasOne(d => d.ActivityDetail).WithOne(p => p.Activity)
                .HasForeignKey<ResourceActivity>(d => d.ActivityDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_activity_detail_id");

            builder.HasOne(d => d.ActivityType).WithMany(p => p.ResourceActivities)
                .HasForeignKey(d => d.ActivityTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_activity_type_id");

            builder.HasOne(d => d.Resource).WithMany(p => p.ResourceActivities)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_id_activity");
        }
    }
}
