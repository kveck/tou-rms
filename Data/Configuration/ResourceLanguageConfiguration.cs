using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MigrateTOUData.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateTOUData.Data.Configuration
{
    internal class ResourceLanguageConfiguration : IEntityTypeConfiguration<ResourceLanguage>
    {
        public void Configure(EntityTypeBuilder<ResourceLanguage> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_language");

            builder.ToTable("resource_language", "rms");

            builder.Property(e => e.Id).HasColumnName("language_id");
            builder.Property(e => e.LanguageName)
                .IsRequired()
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("language_name");
        }
    }
}
