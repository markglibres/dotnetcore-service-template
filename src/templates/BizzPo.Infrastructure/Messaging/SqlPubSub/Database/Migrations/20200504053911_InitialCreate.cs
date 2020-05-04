using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BizzPo.Infrastructure.SqlPubSub.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "PubSubEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MessageType = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Topic = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PubSubEvents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PubSubEvents");
        }
    }
}
