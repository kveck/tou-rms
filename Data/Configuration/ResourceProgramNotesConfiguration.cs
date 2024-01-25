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

            builder.ToTable("resource_program_note", "rms");

            builder.HasIndex(e => e.ResourceDetailId, "uq_resource_detail_id_notes").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.InternalNotes).HasColumnName("internal_notes");
            builder.Property(e => e.ResourceDetailId).HasColumnName("resource_detail_id");

            builder.HasOne(d => d.ResourceDetail).WithOne(p => p.InternalNotes)
                .HasForeignKey<ResourceProgramNote>(d => d.ResourceDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_id_notes");
        }
    }
}
