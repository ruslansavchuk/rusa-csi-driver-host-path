using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Csi.HostPath.Controller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Volumes",
                columns: table => new
                {
                    VolumeId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Size = table.Column<long>(type: "INTEGER", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: true),
                    AccessType = table.Column<int>(type: "INTEGER", nullable: false),
                    ParentVolumeId = table.Column<string>(type: "TEXT", nullable: true),
                    ParentSnapshotId = table.Column<string>(type: "TEXT", nullable: true),
                    Ephemeral = table.Column<bool>(type: "INTEGER", nullable: false),
                    NodeId = table.Column<string>(type: "TEXT", nullable: true),
                    ReadOnlyAttach = table.Column<bool>(type: "INTEGER", nullable: false),
                    Attached = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volumes", x => x.VolumeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Volumes_Name",
                table: "Volumes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Volumes");
        }
    }
}
