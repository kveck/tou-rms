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
    internal class ResourceProcessTimeConfiguration : IEntityTypeConfiguration<ResourceProcessTime>
    {
        public void Configure(EntityTypeBuilder<ResourceProcessTime> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_process_id");

            builder.ToTable("resource_process_time", "rms");

            builder.HasIndex(e => e.TimeCategory, "uq_time_category").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.TimeCategory)
                .HasMaxLength(50)
                .HasColumnName("time_category");
        }
    }
}
