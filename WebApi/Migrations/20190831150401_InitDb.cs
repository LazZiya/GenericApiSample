using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Treasures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Kind = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treasures", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Level", "Name" },
                values: new object[,]
                {
                    { "68ae6a34-6e23-4bdd-967c-2b7e27f67d08", 9, "Laz Ziya" },
                    { "12338796-7482-4863-8ea2-9fb783bcf198", 3, "Ali" },
                    { "2afc4226-021a-467e-8fcf-0f77117f3be5", 2, "Joe" },
                    { "e9de70b2-7f4a-49f0-880e-611927b27a96", 0, "Jasmin" },
                    { "80c41d65-5a38-44da-8ae7-ad396fa0439b", 2, "Michale" },
                    { "630fc3ba-e984-4539-b54a-7a9789dae228", 8, "Lokman" },
                    { "d4b45612-fbd4-44dd-a6d2-a7f6e92de8f9", 7, "Fantom" },
                    { "a9257516-b5c0-496c-bbaa-6d3f3524a43e", 6, "DarkNight" }
                });

            migrationBuilder.InsertData(
                table: "Treasures",
                columns: new[] { "Id", "Kind", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Gold and diamond", "Crown", 999.0 },
                    { 2, "Silver", "Sword", 700.0 },
                    { 3, "Glass", "Lamp", 5.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Treasures");
        }
    }
}
