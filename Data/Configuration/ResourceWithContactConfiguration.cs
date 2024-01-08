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
    internal class ResourceWithContactConfiguration : IEntityTypeConfiguration<ResourceWithContact>
    {
        public void Configure(EntityTypeBuilder<ResourceWithContact> builder) 
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_contact_xref");

            builder.ToTable("resource_contact_xref");

            builder.HasIndex(e => new { e.ContactId, e.ResourceId }, "uq_resource_contact").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ContactId).HasColumnName("contact_id");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");

            builder.HasOne(d => d.Contact).WithMany(p => p.ContactWithResources)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_id_contact");

            builder.HasOne(d => d.Resource).WithMany(p => p.ResourceWithContacts)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contact_id_resource");

        }
    }
}
