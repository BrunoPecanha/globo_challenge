using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pecanha.Repository.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scene",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    OperationHour = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RegisteringDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scene", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecordHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OperationHour = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PreviousState = table.Column<int>(type: "INTEGER", nullable: false),
                    ActualState = table.Column<int>(type: "INTEGER", nullable: false),
                    SceneId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordHistory_Scene_SceneId",
                        column: x => x.SceneId,
                        principalTable: "Scene",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordHistory_SceneId",
                table: "RecordHistory",
                column: "SceneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordHistory");

            migrationBuilder.DropTable(
                name: "Scene");
        }
    }
}
