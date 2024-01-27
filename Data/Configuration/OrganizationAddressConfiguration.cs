using Microsoft.EntityFrameworkCore;
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
    internal class OrganizationAddressConfiguration : IEntityTypeConfiguration<OrganizationAddress>
    {
        public void Configure(EntityTypeBuilder<OrganizationAddress> builder)
        {
                builder.HasKey(e => e.Id).HasName("pk_org_address_id");

                builder.ToTable("organization_address", "rms");

                builder.Property(e => e.Id).HasColumnName("id");
                builder.Property(e => e.Country)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("country");
                builder.Property(e => e.OrgId).HasColumnName("org_id");
                builder.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("state_code");
                builder.Property(e => e.Street1)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("street1");
                builder.Property(e => e.Street2)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("street2");
                builder.Property(e => e.Zip)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("zip");
                builder.Property(e => e.ZipExt)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("zip_ext");

                builder.HasOne(d => d.Org).WithMany(p => p.OrganizationAddresses)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_organization_id");            
        }
    }
}
