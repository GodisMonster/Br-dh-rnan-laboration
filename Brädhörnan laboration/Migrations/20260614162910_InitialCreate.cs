using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brädhörnan_laboration.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameMeetings",
                columns: table => new
                {
                    GameMeetingId = table.Column<int>(type: "int", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaximumNumberOfParticipants = table.Column<int>(type: "int", nullable: false),
                    ResponsibleMemberNumber = table.Column<int>(type: "int", nullable: true),
                    EventType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMeetings", x => x.GameMeetingId);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    GameName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinimumNumberOfPlayer = table.Column<int>(type: "int", nullable: false),
                    MaximumNumberOfPlayer = table.Column<int>(type: "int", nullable: false),
                    AverageGameLength = table.Column<int>(type: "int", nullable: false),
                    GameDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DifficultyLevel = table.Column<int>(type: "int", nullable: false),
                    Gamegenre = table.Column<int>(type: "int", nullable: false),
                    GameAvailability = table.Column<int>(type: "int", nullable: false),
                    GameMeetingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_GameMeetings_GameMeetingId",
                        column: x => x.GameMeetingId,
                        principalTable: "GameMeetings",
                        principalColumn: "GameMeetingId");
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberNumber = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    GameMeetingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberNumber);
                    table.ForeignKey(
                        name: "FK_Members_GameMeetings_GameMeetingId",
                        column: x => x.GameMeetingId,
                        principalTable: "GameMeetings",
                        principalColumn: "GameMeetingId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameMeetings_ResponsibleMemberNumber",
                table: "GameMeetings",
                column: "ResponsibleMemberNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameMeetingId",
                table: "Games",
                column: "GameMeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_GameMeetingId",
                table: "Members",
                column: "GameMeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameMeetings_Members_ResponsibleMemberNumber",
                table: "GameMeetings",
                column: "ResponsibleMemberNumber",
                principalTable: "Members",
                principalColumn: "MemberNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameMeetings_Members_ResponsibleMemberNumber",
                table: "GameMeetings");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "GameMeetings");
        }
    }
}
