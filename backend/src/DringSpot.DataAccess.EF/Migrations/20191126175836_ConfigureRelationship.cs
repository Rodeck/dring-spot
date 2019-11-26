using Microsoft.EntityFrameworkCore.Migrations;

namespace DringSpot.DataAccess.EF.Migrations
{
    public partial class ConfigureRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Places_MeetingPlaceId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MeetingPlaceId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MeetingPlaceId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryPlace",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    MeetingPlaceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPlace", x => new { x.CategoryId, x.MeetingPlaceId });
                    table.ForeignKey(
                        name: "FK_CategoryPlace_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPlace_Places_MeetingPlaceId",
                        column: x => x.MeetingPlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlacesWithin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacesWithin", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPlace_MeetingPlaceId",
                table: "CategoryPlace",
                column: "MeetingPlaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryPlace");

            migrationBuilder.DropTable(
                name: "PlacesWithin");

            migrationBuilder.AddColumn<int>(
                name: "MeetingPlaceId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MeetingPlaceId",
                table: "Categories",
                column: "MeetingPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Places_MeetingPlaceId",
                table: "Categories",
                column: "MeetingPlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
