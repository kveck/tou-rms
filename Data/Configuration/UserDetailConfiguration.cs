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
    internal class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
    {
        public void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_user_details_id");

            builder.ToTable("user_detail");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(320)
                .IsUnicode(false)
                .HasColumnName("email");
            builder.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            builder.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            builder.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("phone");
            builder.Property(e => e.PhotoLocation)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("photo_location");
            builder.Property(e => e.ResourceUserId).HasColumnName("resource_user_id");

            builder.HasOne(d => d.ResourceUser).WithOne(p => p.Detail)                
                .HasForeignKey<UserDetail>(d => d.ResourceUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_id_details");
        }
    }
}
