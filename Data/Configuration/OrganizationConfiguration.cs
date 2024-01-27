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
    internal class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("organization", "rms");
            builder.HasKey(org => org.Id).HasName("pk_org_id");
            builder.Property(org => org.Id)
                .HasColumnName("org_id")
                .IsRequired(true)
                .ValueGeneratedOnAdd();
            builder.Property(org => org.Name)
                .HasColumnName("org_name")
                .IsRequired(true)
                .IsUnicode(false)
                .HasMaxLength(256);
            builder.Property(org => org.WebsiteUrl)
                .HasColumnName("website_url")
                .IsRequired(false)
                .IsUnicode(false)
                .HasMaxLength(2083);
            builder.Property(org => org.Email)
                .HasColumnName("email")
                .IsRequired(false)
                .IsUnicode(false)
                .HasMaxLength(320);
            builder.Property(org => org.Phone)
                .HasColumnName("phone")
                .IsRequired(false)
                .IsUnicode(false)
                .HasMaxLength(10)
                .IsFixedLength();
            builder.Property(org => org.Fax)
                .HasColumnName("fax")
                .IsRequired(false)
                .IsUnicode(false)
                .HasMaxLength(10)
                .IsFixedLength();
        }
    }
}
