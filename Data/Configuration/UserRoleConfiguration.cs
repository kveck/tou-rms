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
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_user_role_id");

            builder.ToTable("user_vss_role");

            builder.HasIndex(e => e.Role, "uq_role").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("user_role");
        }
    }
}
