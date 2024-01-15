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
    internal class ResourceWithSituationConfiguration : IEntityTypeConfiguration<ResourceWithSituation>
    {
        public void Configure(EntityTypeBuilder<ResourceWithSituation> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_situationtaxon_resource_xref");

            builder.ToTable("situation_taxonomy_resource_xref", "rms");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");
            builder.Property(e => e.SituationTaxonomyId).HasColumnName("situation_taxonomy_id");

            builder.HasOne(d => d.Resource).WithMany(p => p.ResourceWithSituations)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_vss_resource_id_situation_taxonomy");

            builder.HasOne(d => d.SituationTaxonomy).WithMany(p => p.SituationsWithResources)
                .HasForeignKey(d => d.SituationTaxonomyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_situation_taxon_id");
        }
    }
}
