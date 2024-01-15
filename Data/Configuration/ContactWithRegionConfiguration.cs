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
    internal class ContactWithRegionConfiguration : IEntityTypeConfiguration<ContactWithRegion>
    {
        public void Configure(EntityTypeBuilder<ContactWithRegion> builder) 
        {
            builder.HasKey(e => e.Id).HasName("pk_regiontaxon_contact_xref");

            builder.ToTable("region_taxonomy_contact_xref", "rms");

            builder.HasIndex(e => new { e.RegionTaxonomyId, e.ContactId }, "uq_contact_region").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ContactId).HasColumnName("contact_id");
            builder.Property(e => e.RegionTaxonomyId).HasColumnName("region_taxonomy_id");

            builder.HasOne(d => d.Contact).WithMany(p => p.ContactWithRegions)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contact_id_region");

            builder.HasOne(d => d.RegionTaxonomy).WithMany(p => p.RegionsWithContacts)
                .HasForeignKey(d => d.RegionTaxonomyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_region_taxon_id_contact");

        }
    }
}
