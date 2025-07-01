using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    HasLicense = table.Column<bool>(type: "bit", nullable: false),
                    BankFounderFullName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    BankDirectorFullName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Capitalization = table.Column<double>(type: "float", nullable: false),
                    EmployeesCount = table.Column<int>(type: "int", nullable: false),
                    BlockedClientsCount = table.Column<int>(type: "int", nullable: false),
                    ClientsCount = table.Column<int>(type: "int", nullable: false),
                    ActiveClientsCount = table.Column<int>(type: "int", nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstablishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LegalAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardTariffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidityPeriod = table.Column<double>(type: "float", nullable: false),
                    MaxCreditLimit = table.Column<int>(type: "int", nullable: false),
                    EnabledPaymentSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestRate = table.Column<double>(type: "float", nullable: true),
                    Curency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnnualMaintenanceCost = table.Column<int>(type: "int", nullable: false),
                    P2PToAnotherBankCommission = table.Column<double>(type: "float", nullable: false),
                    P2PInternalCommission = table.Column<double>(type: "float", nullable: false),
                    CardNumberMasked = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardTariffs_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditTariffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreditName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonthlyInterestRate = table.Column<double>(type: "float", nullable: false),
                    AvailableCreditPeriods = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxCreditAmount = table.Column<double>(type: "float", nullable: false),
                    MinCreditAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditTariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditTariffs_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepositTariffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepositName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnnualInterestRate = table.Column<double>(type: "float", nullable: false),
                    AvailableDepositPeriods = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinDepositAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositTariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepositTariffs_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancalNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    BankPassword = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    ProfilePicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentPhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserNationality = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserGender = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardTariffsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    ChosenCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChosedPaymentSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PinTriesLeft = table.Column<int>(type: "int", nullable: false),
                    PinUnlockTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Cvv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardTarrifsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCards_CardTariffs_CardTariffsId",
                        column: x => x.CardTariffsId,
                        principalTable: "CardTariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardTariffs_BankId",
                table: "CardTariffs",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditTariffs_BankId",
                table: "CreditTariffs",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositTariffs_BankId",
                table: "DepositTariffs",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCards_CardTariffsId",
                table: "UserCards",
                column: "CardTariffsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCards_UserId",
                table: "UserCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BankId",
                table: "Users",
                column: "BankId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditTariffs");

            migrationBuilder.DropTable(
                name: "DepositTariffs");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "UserCards");

            migrationBuilder.DropTable(
                name: "CardTariffs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Banks");
        }
    }
}
