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
    internal class ResourceWithLanguageConfiguration : IEntityTypeConfiguration<ResourceWithLanguage>
    {
        public void Configure(EntityTypeBuilder<ResourceWithLanguage> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_language_xref");

            builder.ToTable("resource_language_xref");

            builder.HasIndex(e => new { e.LanguageId, e.ResourceId }, "uq_resource_language").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.LanguageId).HasColumnName("language_id");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");

            builder.HasOne(d => d.Language).WithMany(p => p.LanguageWithResources)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_language_id");

            builder.HasOne(d => d.Resource).WithMany(p => p.ResourceWithLanguages)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_id_language");
        }
    }
}
