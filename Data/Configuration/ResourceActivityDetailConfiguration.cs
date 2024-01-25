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
    internal class ResourceActivityDetailConfiguration : IEntityTypeConfiguration<ResourceActivityDetail>
    {
        public void Configure(EntityTypeBuilder<ResourceActivityDetail> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_resource_activity_detail");

            builder.ToTable("resource_activity_detail", "rms");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ActivityDetail)
                .IsRequired()
                .HasColumnName("activity_detail");
            builder.Property(e => e.ActivityId).HasColumnName("activity_id");
            builder.Property(e => e.Timestamp)
                .HasPrecision(3)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("timestamp");

            builder.HasOne(d => d.Activity).WithOne(p => p.ActivityDetail)
                .HasForeignKey<ResourceActivityDetail>(d => d.ActivityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_activity_id_detail");
        }
    }
}
