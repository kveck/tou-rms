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
    internal class UserSecurityConfiguration : IEntityTypeConfiguration<UserSecurity>
    {
        public void Configure(EntityTypeBuilder<UserSecurity> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_user_security_id");

            builder.ToTable("user_security");

            builder.HasIndex(e => e.ResourceUserId, "uq_user_id_security").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(576)
                .IsFixedLength()
                .HasColumnName("password");
            builder.Property(e => e.ResourceUserId).HasColumnName("resource_user_id");
            builder.Property(e => e.TimestampLastLogin)
                .HasPrecision(3)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("timestamp_last_login");

            builder.HasOne(d => d.ResourceUser).WithOne(p => p.UserSecurity)
                .HasForeignKey<UserSecurity>(d => d.ResourceUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_id_security");
        }
    }
}
