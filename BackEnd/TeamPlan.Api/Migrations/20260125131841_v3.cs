using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamPlan.Api.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kanban_Task_TaskId",
                table: "Kanban");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Kanban",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Kanban_TaskId",
                table: "Kanban",
                newName: "IX_Kanban_TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kanban_Team_TeamId",
                table: "Kanban",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kanban_Team_TeamId",
                table: "Kanban");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Kanban",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Kanban_TeamId",
                table: "Kanban",
                newName: "IX_Kanban_TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kanban_Task_TaskId",
                table: "Kanban",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
