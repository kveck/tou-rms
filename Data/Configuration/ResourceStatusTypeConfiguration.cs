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
    internal class ResourceStatusTypeConfiguration : IEntityTypeConfiguration<ResourceStatusType>
    {
        public void Configure(EntityTypeBuilder<ResourceStatusType> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_status_type_id");

            builder.ToTable("resource_status_type", "rms");

            builder.HasIndex(e => e.StatusType, "uq_status").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.StatusType)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("status_type");
        }
    }
}
