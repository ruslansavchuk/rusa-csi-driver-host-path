using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Csi.HostPath.Controller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovaPathProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Volumes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Volumes",
                type: "TEXT",
                nullable: true);
        }
    }
}
