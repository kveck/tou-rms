using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MigrateTOUData.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateTOUData.Data.Configuration
{
    internal class ResourceWithApplicationTypeConfiguration : IEntityTypeConfiguration<ResourceWithApplicationType>
    {
        public void Configure(EntityTypeBuilder<ResourceWithApplicationType> builder) 
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_application_type_xref");

            builder.ToTable("resource_application_type_xref", "rms");

            builder.HasIndex(e => new { e.ApplicationTypeId, e.ResourceId }, "uq_resource_application_type").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ApplicationTypeId).HasColumnName("application_type_id");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");

            builder.HasOne(d => d.ApplicationType).WithMany(p => p.ApplicationTypesWithResources)
                .HasForeignKey(d => d.ApplicationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_application_type_id");

            builder.HasOne(d => d.Resource).WithMany(p => p.ResourceWithApplicationTypes)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_program_id_application_type");
        }
    }
}
