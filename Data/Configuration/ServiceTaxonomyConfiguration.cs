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
    internal class ServiceTaxonomyConfiguration : IEntityTypeConfiguration<ServiceTaxonomy>
    {
        public void Configure(EntityTypeBuilder<ServiceTaxonomy> builder)
        {
            builder.HasKey(e => e.TaxonomyId).HasName("pk_service_taxonomy");

            builder.ToTable("service_taxonomy");

            builder.HasIndex(e => e.TaxonomyLeft, "uq_service_taxonomy_left").IsUnique();

            builder.HasIndex(e => e.TaxonomyRight, "uq_service_taxonomy_right").IsUnique();

            builder.Property(e => e.TaxonomyId).HasColumnName("taxonomy_id");
            builder.Property(e => e.Service)
                .IsRequired()
                .HasMaxLength(126)
                .IsUnicode(false)
                .HasColumnName("service");
            builder.Property(e => e.TaxonomyLeft).HasColumnName("taxonomy_left");
            builder.Property(e => e.TaxonomyRight).HasColumnName("taxonomy_right");
        }
    }
}
