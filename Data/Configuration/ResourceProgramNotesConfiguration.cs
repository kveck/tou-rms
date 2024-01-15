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
    internal class ResourceProgramNotesConfiguration : IEntityTypeConfiguration<ResourceProgramNote>
    {
        public void Configure(EntityTypeBuilder<ResourceProgramNote> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_resource_program_notes");

            builder.ToTable("resource_program_notes", "rms");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.InternalNotes).HasColumnName("internal_notes");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");

            builder.HasOne(d => d.Resource).WithMany(p => p.ResourceProgramNotes)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_id_notes");
        }
    }
}
