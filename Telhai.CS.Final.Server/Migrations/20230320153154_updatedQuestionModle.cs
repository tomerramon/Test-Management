using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telhai.CS.Final.Server.Migrations
{
    /// <inheritdoc />
    public partial class updatedQuestionModle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRandom",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRandom",
                table: "Questions");
        }
    }
}
