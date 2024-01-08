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
    internal class UserWithResourceRatingConfiguration : IEntityTypeConfiguration<UserWithResourceRating>
    {
        public void Configure(EntityTypeBuilder<UserWithResourceRating> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_user_resource_xref_id");

            builder.ToTable("user_resource_rating");

            builder.HasIndex(e => new { e.ResourceUserId, e.ResourceId }, "uq_user_resource_rating").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Details)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("details");
            builder.Property(e => e.Rating).HasColumnName("rating");
            builder.Property(e => e.ResourceId).HasColumnName("resource_id");
            builder.Property(e => e.ResourceUserId).HasColumnName("resource_user_id");
            builder.Property(e => e.TimestampCreated)
                .HasPrecision(3)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("timestamp_created");
            builder.Property(e => e.TimestampLastUpdate)
                .HasPrecision(3)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("timestamp_last_update");

            builder.HasOne(d => d.Resource).WithMany(p => p.ResourceWithUserRatings)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resource_id_user");

            builder.HasOne(d => d.ResourceUser).WithMany(p => p.UserWithResourceRatings)
                .HasForeignKey(d => d.ResourceUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_id_resource_rating");
        }
    }
}
