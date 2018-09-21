using Microsoft.EntityFrameworkCore.Migrations;

namespace ORMDemo.EF.Migrations
{
    public partial class TestRelationshipConventions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Posts",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Blogs",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PostExtensions",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false),
                    ExtensionField1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostExtensions", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_PostExtensions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostExtensions");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Blogs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);
        }
    }
}
