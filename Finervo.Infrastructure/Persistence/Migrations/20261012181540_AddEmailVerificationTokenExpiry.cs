using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finervo.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailVerificationTokenExpiry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email_verification_token",
                table: "users",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "email_verification_token_expiry",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_email_verified",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email_verification_token",
                table: "users");

            migrationBuilder.DropColumn(
                name: "email_verification_token_expiry",
                table: "users");

            migrationBuilder.DropColumn(
                name: "is_email_verified",
                table: "users");
        }
    }
}
