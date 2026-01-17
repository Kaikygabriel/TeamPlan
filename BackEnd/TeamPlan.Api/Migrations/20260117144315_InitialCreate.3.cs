using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamPlan.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Owner_EnterpriseId",
                table: "Owner");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnterpriseId",
                table: "Owner",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_EnterpriseId",
                table: "Owner",
                column: "EnterpriseId",
                unique: true,
                filter: "[EnterpriseId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Owner_EnterpriseId",
                table: "Owner");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnterpriseId",
                table: "Owner",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owner_EnterpriseId",
                table: "Owner",
                column: "EnterpriseId",
                unique: true);
        }
    }
}
