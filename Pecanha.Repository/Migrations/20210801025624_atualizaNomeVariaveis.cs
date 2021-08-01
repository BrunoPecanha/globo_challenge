using Microsoft.EntityFrameworkCore.Migrations;

namespace Pecanha.Repository.Migrations
{
    public partial class atualizaNomeVariaveis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActualState",
                table: "RecordHistory",
                newName: "CurrentState");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentState",
                table: "RecordHistory",
                newName: "ActualState");
        }
    }
}
