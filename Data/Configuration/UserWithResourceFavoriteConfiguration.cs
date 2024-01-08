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
    internal class UserWithResourceFavoriteConfiguration : IEntityTypeConfiguration<UserWithResourceFavorite>
    {
        public void Configure(EntityTypeBuilder<UserWithResourceFavorite> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_user_resource_favorites_id");

            builder.ToTable("user_resource_favorites");

            builder.HasIndex(e => new { e.ResourceUserId, e.ResourceId }, "uq_user_resource_favorites").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Notes)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("notes");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");
            builder.Property(e => e.ResourceUserId).HasColumnName("resource_user_id");

            builder.HasOne(d => d.Resource).WithMany(p => p.ResourceWithUserFavorites)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_id_favorites");

            builder.HasOne(d => d.ResourceUser).WithMany(p => p.UserWithResourceFavorites)
                .HasForeignKey(d => d.ResourceUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_id_resource_favorites");
        }
    }
}
