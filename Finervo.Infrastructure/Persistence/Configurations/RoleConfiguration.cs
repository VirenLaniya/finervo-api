using Finervo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Persistence.Configurations
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .ValueGeneratedNever()
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(r => r.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(r => r.Name)
                .IsUnique()
                .HasDatabaseName("uq_roles_name");

            builder.Property(r => r.Description)
                .HasColumnName("description")
                .HasMaxLength(250);

            builder.Property(r => r.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            builder.Property(r => r.IsSystem)
                .HasColumnName("is_system")
                .HasDefaultValue(false);

            builder.Property(r => r.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(r => r.LastUpdatedAt)
                .HasColumnName("last_updated")
                .IsRequired();
        }
    }
}
