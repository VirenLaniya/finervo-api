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
                .HasColumnName("id");

            builder.Property(u => u.FirstName)
                .HasColumnName("first_name");

            builder.Property(u => u.LastName)
                .HasColumnName("last_name");

            builder.Property(u => u.Email)
                .HasColumnName("email");
            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("un_users_email");

            builder.Property(u => u.UserName)
                .HasColumnName("username");

            builder.Property(u => u.Password)
                .HasColumnName("password");

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at");

            builder.Property(u => u.LastUpdatedAt)
                .HasColumnName("last_updated");
        }
    }
}
