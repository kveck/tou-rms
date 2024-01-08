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
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_user_id");

            builder.ToTable("user_vss");

            builder.HasIndex(e => e.ResourceUserId, "uq_user_id").IsUnique();

            builder.HasIndex(e => e.Username, "uq_username").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ResourceUserId)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("resource_user_id");
            builder.Property(e => e.RoleId).HasColumnName("role_id");
            builder.Property(e => e.TimestampCreated)
                .HasPrecision(3)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("timestamp_created");
            builder.Property(e => e.TimestampLastUpdate)
                .HasPrecision(3)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("timestamp_last_update");
            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("username");

            builder.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_role_id");
        }
    }
}
