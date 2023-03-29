using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatRAndRecordTypes.Api.Migrations
{
    /// <inheritdoc />
    public partial class MediatRAndRecordTypes_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MediatRAndRecordTypes");

            migrationBuilder.CreateTable(
                name: "Consults",
                schema: "MediatRAndRecordTypes",
                columns: table => new
                {
                    ConsultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateRange_StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRange_EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consults", x => x.ConsultId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consults",
                schema: "MediatRAndRecordTypes");
        }
    }
}
