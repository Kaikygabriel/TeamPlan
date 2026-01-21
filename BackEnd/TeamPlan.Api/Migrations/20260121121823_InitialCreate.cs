using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamPlan.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enterprise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IdOwner = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(180)", maxLength: 180, nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(170)", maxLength: 170, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnterpriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Owner_Enterprise",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Owner_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false),
                    CommentParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_CommentParentId",
                        column: x => x.CommentParentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mark",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Descriptor = table.Column<string>(type: "VARCHAR(180)", maxLength: 180, nullable: false),
                    TaskCount = table.Column<short>(type: "SMALLINT", nullable: false, defaultValue: (short)0),
                    TaskCountDone = table.Column<short>(type: "SMALLINT", nullable: false, defaultValue: (short)0),
                    Percentage = table.Column<byte>(type: "TINYINT", nullable: false, defaultValue: (byte)0),
                    Done = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mark", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(180)", maxLength: 180, nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Role = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    ManagedTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnterpriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PercentageByMonthCurrent = table.Column<byte>(type: "TINYINT", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Enterprise",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Team_Manager",
                        column: x => x.ManagerId,
                        principalTable: "Member",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecurringTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayMonth = table.Column<byte>(type: "TINYINT", nullable: false),
                    DaysActiveTask = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(180)", maxLength: 180, nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringTask_Team",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Percentage = table.Column<byte>(type: "TINYINT", nullable: false, defaultValue: (byte)0),
                    Title = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    Active = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Priority = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Member",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Task_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentParentId",
                table: "Comments",
                column: "CommentParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MemberId",
                table: "Comments",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Task",
                table: "Comments",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Mark_TeamId",
                table: "Mark",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_ManagedTeamId",
                table: "Member",
                column: "ManagedTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_TeamId",
                table: "Member",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_UserId",
                table: "Member",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_EnterpriseId",
                table: "Owner",
                column: "EnterpriseId",
                unique: true,
                filter: "[EnterpriseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_UserId",
                table: "Owner",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTask_TeamId",
                table: "RecurringTask",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_MemberId",
                table: "Task",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_TeamId",
                table: "Task",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_EnterpriseId",
                table: "Team",
                column: "EnterpriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_ManagerId",
                table: "Team",
                column: "ManagerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Member",
                table: "Comments",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Task",
                table: "Comments",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Team",
                table: "Mark",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Team",
                table: "Member",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Team_ManagedTeamId",
                table: "Member",
                column: "ManagedTeamId",
                principalTable: "Team",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Manager",
                table: "Team");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Mark");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropTable(
                name: "RecurringTask");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Enterprise");
        }
    }
}
