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
    internal class ResourceApplicationTypeConfiguration : IEntityTypeConfiguration<ResourceApplicationType>
    {
        public void Configure(EntityTypeBuilder<ResourceApplicationType> builder) 
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_application_type");

            builder.ToTable("resource_application_type", "rms");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ApplicationType)
                .IsRequired()
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("application_type");
        }
    }
}
