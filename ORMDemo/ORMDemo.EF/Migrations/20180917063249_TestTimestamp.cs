using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ORMDemo.EF.Migrations
{
    public partial class TestTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Blogs",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Blogs");
        }
    }
}
