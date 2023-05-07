using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfoAPI.Migrations
{
    /// <inheritdoc />
    public partial class addPointOfInterest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PointOfInterests",
                columns: table => new
                {
                    PointOfInterestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PointOfInterestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointOfInterestDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointOfInterests", x => x.PointOfInterestId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointOfInterests");
        }
    }
}
