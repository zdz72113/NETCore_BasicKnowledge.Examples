using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ORMDemo.MultiTenancy.Migrations.Tenants
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Host = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Host", "Name" },
                values: new object[] { new Guid("b992d195-56ce-49bf-bfdd-4145ba9a0c13"), "localhost:5200", "Customer A" });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Host", "Name" },
                values: new object[] { new Guid("f55ae0c8-4573-4a0a-9ef9-32f66a828d0e"), "localhost:5300", "Customer B" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
