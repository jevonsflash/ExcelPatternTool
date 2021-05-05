using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Core.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSocialInsuranceAndFund",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSocialInsuranceDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "EnterpriseSocialInsuranceAndFund",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "SalayInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                });

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
                    EmployeeAccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeSalayId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeSocialInsuranceAndFundId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EnterpriseSocialInsuranceAndFundId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeSocialInsuranceDetailId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_EmployeeAccount_EmployeeAccountId",
                        column: x => x.EmployeeAccountId,
                        principalTable: "EmployeeAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_EmployeeSocialInsuranceAndFund_EmployeeSocialInsuranceAndFundId",
                        column: x => x.EmployeeSocialInsuranceAndFundId,
                        principalTable: "EmployeeSocialInsuranceAndFund",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_EmployeeSocialInsuranceDetail_EmployeeSocialInsuranceDetailId",
                        column: x => x.EmployeeSocialInsuranceDetailId,
                        principalTable: "EmployeeSocialInsuranceDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_EnterpriseSocialInsuranceAndFund_EnterpriseSocialInsuranceAndFundId",
                        column: x => x.EnterpriseSocialInsuranceAndFundId,
                        principalTable: "EnterpriseSocialInsuranceAndFund",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_SalayInfo_EmployeeSalayId",
                        column: x => x.EmployeeSalayId,
                        principalTable: "SalayInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeAccountId",
                table: "Employee",
                column: "EmployeeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeSalayId",
                table: "Employee",
                column: "EmployeeSalayId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeSocialInsuranceAndFundId",
                table: "Employee",
                column: "EmployeeSocialInsuranceAndFundId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeSocialInsuranceDetailId",
                table: "Employee",
                column: "EmployeeSocialInsuranceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EnterpriseSocialInsuranceAndFundId",
                table: "Employee",
                column: "EnterpriseSocialInsuranceAndFundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

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
        }
    }
}
