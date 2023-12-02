using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMovies.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "invoice");

            migrationBuilder.CreateSequence<int>(
                name: "InvoiceNumber",
                schema: "invoice");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceNumber",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValueSql: "Next Value For invoice.InvoiceNumber");

            //migrationBuilder.UpdateData(
            //    table: "Invoices",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    columns: new string[0],
            //    values: new object[0]);

            //migrationBuilder.UpdateData(
            //    table: "Invoices",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    columns: new string[0],
            //    values: new object[0]);

            //migrationBuilder.UpdateData(
            //    table: "Invoices",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    columns: new string[0],
            //    values: new object[0]);

            //migrationBuilder.UpdateData(
            //    table: "Invoices",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    columns: new string[0],
            //    values: new object[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Invoices");

            migrationBuilder.DropSequence(
                name: "InvoiceNumber",
                schema: "invoice");
        }
    }
}
