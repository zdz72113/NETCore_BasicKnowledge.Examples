using Microsoft.EntityFrameworkCore.Migrations;

namespace ORMDemo.EF.Migrations
{
    public partial class TestSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "BlogId", "Rating", "Url" },
                values: new object[] { 1, 0, "http://sample.com/1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 1);
        }
    }
}
