using Finervo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Persistence.Configurations
{
    internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("user_roles");

            // Composite Key - UserId + RoleId
            builder.HasKey(u => new { u.UserId, u.RoleId });

            builder.Property(ur => ur.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(ur => ur.RoleId)
                .HasColumnName("role_id")
                .IsRequired();

            builder.Property(ur => ur.AssignedBy)
                .HasColumnName("assigned_by")
                .IsRequired();

            builder.Property(ur => ur.AssignedAt)
                .HasColumnName("assigned_at")
                .IsRequired();

            // Relationsships
            builder.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles) 
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
