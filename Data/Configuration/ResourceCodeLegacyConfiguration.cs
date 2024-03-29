﻿using Microsoft.EntityFrameworkCore;
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
    internal class ResourceCodeLegacyConfiguration : IEntityTypeConfiguration<ResourceCodeLegacy>
    {
        public void Configure(EntityTypeBuilder<ResourceCodeLegacy> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_code_legacy");

            builder.ToTable("resource_code_legacy", "rms");

            builder.HasIndex(e => e.LegacyCode, "uq_legacy_code").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.LegacyCode).HasColumnName("legacy_code");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");

            builder.HasOne(d => d.Resource).WithMany(p => p.ResourceCodeLegacies)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_id_legacy");

        }
    }
}
