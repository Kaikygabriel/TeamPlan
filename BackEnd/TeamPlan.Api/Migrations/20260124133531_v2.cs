using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamPlan.Api.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Task",
                type: "SMALLDATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Task",
                type: "SMALLDATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<byte>(
                name: "KanbanCurrent",
                table: "Task",
                type: "TINYINT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Kanban",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KanbanTitle = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    KanbanOrder = table.Column<byte>(type: "TINYINT", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kanban", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kanban_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_Name",
                table: "Member",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kanban_TaskId",
                table: "Kanban",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kanban");

            migrationBuilder.DropIndex(
                name: "IX_Member_Name",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "KanbanCurrent",
                table: "Task");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Task",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "SMALLDATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Task",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "SMALLDATETIME");
        }
    }
}
