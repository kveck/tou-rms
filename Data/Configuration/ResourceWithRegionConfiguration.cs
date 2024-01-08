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
    internal class ResourceWithRegionConfiguration : IEntityTypeConfiguration<ResourceWithRegion>
    {
        public void Configure(EntityTypeBuilder<ResourceWithRegion> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_regiontaxon_resource_xref");

            builder.ToTable("region_taxonomy_resource_xref");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.RegionTaxonomyId).HasColumnName("region_taxonomy_id");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");

            builder.HasOne(d => d.RegionTaxonomy).WithMany(p => p.RegionWithResources)
                .HasForeignKey(d => d.RegionTaxonomyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_region_taxon_id");

            builder.HasOne(d => d.Resource).WithMany(p => p.ResourceWithRegions)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_program_id_region_taxonomy");
        }
    }
}
