﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MigrateTOUData.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateTOUData.Data.Configuration
{
    internal class SituationTaxonomyConfiguration : IEntityTypeConfiguration<SituationTaxonomy>
    {
        public void Configure(EntityTypeBuilder<SituationTaxonomy> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_situation_taxonomy");

            builder.ToTable("situation_taxonomy", "rms");

            builder.HasIndex(e => e.TaxonomyLeft, "uq_situation_taxonomy_left").IsUnique();

            builder.HasIndex(e => e.TaxonomyRight, "uq_situation_taxonomy_right").IsUnique();

            builder.Property(e => e.Id).HasColumnName("taxonomy_id");
            builder.Property(e => e.Situation)
                .IsRequired()
                .HasMaxLength(126)
                .IsUnicode(false)
                .HasColumnName("situation");
            builder.Property(e => e.TaxonomyLeft)
                .IsRequired()
                .HasColumnName("taxonomy_left");
            builder.Property(e => e.TaxonomyRight)
                .IsRequired()
                .HasColumnName("taxonomy_right");
        }
    }
}
