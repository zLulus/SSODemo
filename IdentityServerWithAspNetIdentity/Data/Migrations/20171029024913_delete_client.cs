using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IdentityServerWithAspNetIdentity.Data.Migrations
{
    public partial class delete_client : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityServer4Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityServer4Clients",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowOfflineAccess = table.Column<bool>(nullable: false),
                    ClientId = table.Column<string>(nullable: true),
                    ClientName = table.Column<string>(nullable: true),
                    DictionaryJson = table.Column<string>(nullable: true),
                    RequireConsent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityServer4Clients", x => x.Id);
                });
        }
    }
}
