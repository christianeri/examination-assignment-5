using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SupportTicketManager.Migrations
{
    /// <inheritdoc />
    public partial class InitlatestDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PropertyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CustomerPhone = table.Column<string>(type: "char(13)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TicketDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    TicketCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketReference = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    TicketComment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TicketCommentUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuildingId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "BuildingName", "PropertyCode" },
                values: new object[,]
                {
                    { 1, "Blåsenhus", "5:1" },
                    { 2, "Carolina Rediviva", "1:68" },
                    { 3, "Ekonomikum", "62:8" },
                    { 4, "Rudbecklaboratoriet", "1:23" },
                    { 5, "Ångströmlaboratoriet", "7:1" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "TicketStatus" },
                values: new object[,]
                {
                    { 1, "Ej Påbörjad" },
                    { 2, "Pågående" },
                    { 3, "Avslutad" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerEmail",
                table: "Customers",
                column: "CustomerEmail",
                unique: true,
                filter: "[CustomerEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_BuildingId",
                table: "Tickets",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CustomerId",
                table: "Tickets",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_StatusId",
                table: "Tickets",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketReference",
                table: "Tickets",
                column: "TicketReference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
