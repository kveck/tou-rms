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
    internal class ResourceActivityTypeConfiguration : IEntityTypeConfiguration<ResourceActivityType>
    {
        public void Configure(EntityTypeBuilder<ResourceActivityType> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_activity_type");

            builder.ToTable("resource_activity_type", "rms");

            builder.HasIndex(e => e.ActivityType, "uq_activity_type").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ActivityType)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("activity_type");
        }
    }
}
