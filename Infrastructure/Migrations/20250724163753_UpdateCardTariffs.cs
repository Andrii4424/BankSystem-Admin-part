using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCardTariffs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnabledPaymentSystem",
                table: "CardTariffs",
                newName: "EnabledPaymentSystems");

            migrationBuilder.RenameColumn(
                name: "Curency",
                table: "CardTariffs",
                newName: "EnableCurency");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "CardTariffs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "CardTariffs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnabledPaymentSystems",
                table: "CardTariffs",
                newName: "EnabledPaymentSystem");

            migrationBuilder.RenameColumn(
                name: "EnableCurency",
                table: "CardTariffs",
                newName: "Curency");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "CardTariffs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "CardTariffs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
