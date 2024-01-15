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
    internal class ContactWithLanguageConfiguration : IEntityTypeConfiguration<ContactWithLanguage>
    {
        public void Configure(EntityTypeBuilder<ContactWithLanguage> builder) 
        {
            builder.HasKey(e => e.Id).HasName("pk_contact_language_xref");

            builder.ToTable("contact_language_xref", "rms");

            builder.HasIndex(e => new { e.LanguageId, e.ContactId }, "uq_contact_language").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ContactId).HasColumnName("contact_id");
            builder.Property(e => e.LanguageId).HasColumnName("language_id");

            builder.HasOne(d => d.Contact).WithMany(p => p.ContactWithLanguages)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contact_id_language");

            builder.HasOne(d => d.Language).WithMany(p => p.LanguageWithContacts)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_language_id_contact");

        }
    }
}
