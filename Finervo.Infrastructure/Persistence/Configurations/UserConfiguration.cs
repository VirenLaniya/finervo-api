using Finervo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .ValueGeneratedNever()
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(u => u.FirstName)
                .HasColumnName("first_name")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.LastName)
                .HasColumnName("last_name")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(256);

            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("un_users_email");

            builder.Property(u => u.UserName)
                .HasColumnName("username")
                .IsRequired()
                .HasMaxLength(40);

            builder.HasIndex(u => u.UserName)
                .IsUnique()
                .HasDatabaseName("un_users_username");

            builder.Property(u => u.Password)
                .HasColumnName("password")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.RefreshToken)
                .HasColumnName("refresh_token")
                .HasMaxLength(512);

            builder.Property(u => u.RefreshTokenExpiryTime)
                .HasColumnName("refresh_token_expiry");

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(u => u.LastUpdatedAt)
                .HasColumnName("last_updated")
                .IsRequired();
        }
    }
}
