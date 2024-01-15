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
    internal class ResourceWithServiceConfiguration : IEntityTypeConfiguration<ResourceWithService>
    {
        public void Configure(EntityTypeBuilder<ResourceWithService> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_servicetaxon_resource_xref");

            builder.ToTable("service_taxonomy_resource_xref", "rms");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");
            builder.Property(e => e.ServiceTaxonomyId).HasColumnName("service_taxonomy_id");

            builder.HasOne(d => d.Resource).WithMany(p => p.ResourceWithServices)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_vss_resource_id_service_taxonomy");

            builder.HasOne(d => d.ServiceTaxonomy).WithMany(p => p.ServicesWithResources)
                .HasForeignKey(d => d.ServiceTaxonomyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_service_taxon_id");
        }
    }
}
