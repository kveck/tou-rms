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
    internal class ResourceContactConfiguration : IEntityTypeConfiguration<ResourceContact>
    {
        public void Configure(EntityTypeBuilder<ResourceContact> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_contact");

            builder.ToTable("resource_contact", "rms");

            builder.Property(e => e.Id).HasColumnName("contact_id");
            builder.Property(e => e.OrgId).HasColumnName("org_id");
            builder.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false)
                .HasColumnName("email");
            builder.Property(e => e.FirstName)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("first_name");
            builder.Property(e => e.LastName)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("last_name");
            builder.Property(e => e.MiddleName)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("middle_name");
            builder.Property(e => e.Mobile)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("mobile");
            builder.Property(e => e.OrgTitle)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("org_title");
            builder.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("phone");
            builder.Property(e => e.PhoneExt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone_ext");
            builder.Property(e => e.Fax)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("fax");
            builder.Property(e => e.Suffix)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("suffix");
            builder.Property(e => e.Title)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("title");


            builder.HasOne(d => d.Org).WithMany(p => p.ResourceContacts)
                .HasForeignKey(d => d.OrgId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_org_resource_contact");
        }
    }
}
