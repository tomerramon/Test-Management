using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telhai.CS.Final.Server.Migrations
{
    /// <inheritdoc />
    public partial class updatedTablesParticipation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Strudent_Name",
                table: "Participations",
                newName: "Student_Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Student_Name",
                table: "Participations",
                newName: "Strudent_Name");
        }
    }
}
