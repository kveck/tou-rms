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
    internal class ResourceProgramConfiguration : IEntityTypeConfiguration<ResourceProgram>
    {
        public void Configure(EntityTypeBuilder<ResourceProgram> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_program");

            builder.ToTable("resource_program");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Cost)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("cost");
            builder.Property(e => e.Description)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("description");
            builder.Property(e => e.Name)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("name");
            builder.Property(e => e.Notes)
                .IsUnicode(false)
                .HasColumnName("notes");
            builder.Property(e => e.OrgId)
                .HasColumnName("org_id")
                .IsRequired();
            builder.Property(e => e.ResourceId)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("resource_id");
            builder.Property(e => e.ResourceLink)
                .HasMaxLength(2083)
                .IsUnicode(false)
                .HasColumnName("resource_link");
            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("status");
            builder.Property(e => e.TimestampCreated)
                .HasPrecision(3)
                .HasDefaultValueSql("(getutcdate())")
                .IsRequired()
                .HasColumnName("timestamp_created");
            builder.Property(e => e.TimestampLastUpdate)
                .HasPrecision(3)
                .IsRequired()
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("timestamp_last_update");

            builder.HasOne(d => d.Org).WithMany(p => p.ResourcePrograms)
                .HasForeignKey(d => d.OrgId)
                .HasConstraintName("fk_resource_organization_id");
        }
    }
}
