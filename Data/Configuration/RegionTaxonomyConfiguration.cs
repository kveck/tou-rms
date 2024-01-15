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
    internal class RegionTaxonomyConfiguration : IEntityTypeConfiguration<RegionTaxonomy>
    {
        public void Configure(EntityTypeBuilder<RegionTaxonomy> builder) 
        {
            builder.HasKey(e => e.Id).HasName("pk_region_taxonomy");

            builder.ToTable("region_taxonomy", "rms");

            builder.HasIndex(e => e.TaxonomyLeft, "uq_region_taxonomy_left").IsUnique();

            builder.HasIndex(e => e.TaxonomyRight, "uq_region_taxonomy_right").IsUnique();

            builder.Property(e => e.Id).HasColumnName("taxonomy_id");
            builder.Property(e => e.Region)
                .IsRequired()
                .HasMaxLength(126)
                .IsUnicode(false)
                .HasColumnName("region");
            builder.Property(e => e.TaxonomyLeft)
                .IsRequired()
                .HasColumnName("taxonomy_left");
            builder.Property(e => e.TaxonomyRight)
                .IsRequired()
                .HasColumnName("taxonomy_right");
        }
    }
}
