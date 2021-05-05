using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Core.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Mounth = table.Column<int>(type: "INTEGER", nullable: false),
                    Batch = table.Column<string>(type: "TEXT", nullable: true),
                    SerialNum = table.Column<string>(type: "TEXT", nullable: true),
                    Dept = table.Column<string>(type: "TEXT", nullable: true),
                    Proj = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    IDCard = table.Column<string>(type: "TEXT", nullable: true),
                    Level = table.Column<string>(type: "TEXT", nullable: true),
                    JobCate = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountNum = table.Column<string>(type: "TEXT", nullable: true),
                    AccountBankAlias = table.Column<string>(type: "TEXT", nullable: true),
                    AccountBankName = table.Column<string>(type: "TEXT", nullable: true),
                    AccountBankLoc = table.Column<string>(type: "TEXT", nullable: true),
                    SocialInsuranceNum = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAccount_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSocialInsuranceAndFund",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SocialInsurancePersonal = table.Column<double>(type: "REAL", nullable: false),
                    SupplementarySocialInsurancePersonal = table.Column<double>(type: "REAL", nullable: false),
                    ProvidentFundPersonal = table.Column<double>(type: "REAL", nullable: false),
                    SupplementaryProvidentFundPersonal = table.Column<double>(type: "REAL", nullable: false),
                    BeforeFillingOut = table.Column<double>(type: "REAL", nullable: false),
                    PersonalIncomeTax = table.Column<double>(type: "REAL", nullable: false),
                    UnionFeePersonal = table.Column<double>(type: "REAL", nullable: false),
                    SupplementaryUnionFeeDeductPersonal = table.Column<double>(type: "REAL", nullable: false),
                    SupplementaryCommercialInsuranceDeduct = table.Column<double>(type: "REAL", nullable: false),
                    Sum = table.Column<double>(type: "REAL", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSocialInsuranceAndFund", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSocialInsuranceAndFund_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSocialInsuranceDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BasicOldAgeInsurance = table.Column<double>(type: "REAL", nullable: false),
                    BasicMedicalInsurance = table.Column<double>(type: "REAL", nullable: false),
                    UnemploymentInsurance = table.Column<double>(type: "REAL", nullable: false),
                    Check = table.Column<double>(type: "REAL", nullable: false),
                    ProvidentFund = table.Column<double>(type: "REAL", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSocialInsuranceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSocialInsuranceDetail_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnterpriseSocialInsuranceAndFund",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SocialInsuranceEnterprise = table.Column<double>(type: "REAL", nullable: false),
                    SupplementarySocialInsuranceEnterprise = table.Column<double>(type: "REAL", nullable: false),
                    ProvidentFundEnterprise = table.Column<double>(type: "REAL", nullable: false),
                    SupplementaryProvidentFundEnterprise = table.Column<double>(type: "REAL", nullable: false),
                    UnionFeeEnterprise = table.Column<double>(type: "REAL", nullable: false),
                    SupplementaryUnionFeeDeductEnterprise = table.Column<double>(type: "REAL", nullable: false),
                    Sum = table.Column<double>(type: "REAL", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseSocialInsuranceAndFund", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnterpriseSocialInsuranceAndFund_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalayInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProbationSalary = table.Column<double>(type: "REAL", nullable: false),
                    BasicSalary = table.Column<double>(type: "REAL", nullable: false),
                    SkillSalary = table.Column<double>(type: "REAL", nullable: false),
                    PerformanceBonus = table.Column<double>(type: "REAL", nullable: false),
                    PostAllowance = table.Column<double>(type: "REAL", nullable: false),
                    OtherAllowances = table.Column<double>(type: "REAL", nullable: false),
                    SalesBonus = table.Column<double>(type: "REAL", nullable: false),
                    Bonus1 = table.Column<double>(type: "REAL", nullable: false),
                    Bonus2 = table.Column<double>(type: "REAL", nullable: false),
                    PerformanceRewards = table.Column<double>(type: "REAL", nullable: false),
                    PerformanceDeduct = table.Column<double>(type: "REAL", nullable: false),
                    NightAllowances = table.Column<double>(type: "REAL", nullable: false),
                    OrdinaryOvertime = table.Column<double>(type: "REAL", nullable: false),
                    HolidayOvertime = table.Column<double>(type: "REAL", nullable: false),
                    AgeBonus = table.Column<double>(type: "REAL", nullable: false),
                    AttendanceDeduct = table.Column<double>(type: "REAL", nullable: false),
                    MonthlyAttendance = table.Column<double>(type: "REAL", nullable: false),
                    QuarterlyAttendance = table.Column<double>(type: "REAL", nullable: false),
                    OtherRewards = table.Column<double>(type: "REAL", nullable: false),
                    OtherDeduct = table.Column<double>(type: "REAL", nullable: false),
                    SupplementaryRewards = table.Column<double>(type: "REAL", nullable: false),
                    SupplementaryDeduct = table.Column<double>(type: "REAL", nullable: false),
                    ParttimeSalary = table.Column<double>(type: "REAL", nullable: false),
                    Sum = table.Column<double>(type: "REAL", nullable: false),
                    HostelAllowances = table.Column<double>(type: "REAL", nullable: false),
                    MealAllowances = table.Column<double>(type: "REAL", nullable: false),
                    TemporaryTax = table.Column<double>(type: "REAL", nullable: false),
                    SumWithTemporaryTax = table.Column<double>(type: "REAL", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalayInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalayInfo_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAccount_EmployeeId",
                table: "EmployeeAccount",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSocialInsuranceAndFund_EmployeeId",
                table: "EmployeeSocialInsuranceAndFund",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSocialInsuranceDetail_EmployeeId",
                table: "EmployeeSocialInsuranceDetail",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseSocialInsuranceAndFund_EmployeeId",
                table: "EnterpriseSocialInsuranceAndFund",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalayInfo_EmployeeId",
                table: "SalayInfo",
                column: "EmployeeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeAccount");

            migrationBuilder.DropTable(
                name: "EmployeeSocialInsuranceAndFund");

            migrationBuilder.DropTable(
                name: "EmployeeSocialInsuranceDetail");

            migrationBuilder.DropTable(
                name: "EnterpriseSocialInsuranceAndFund");

            migrationBuilder.DropTable(
                name: "SalayInfo");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
