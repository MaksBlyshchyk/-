using System;
using HRReserveSystem.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRReserveSystem.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20260509181600_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Candidates",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                FullName = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                Email = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                Phone = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                Skills = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                ExperienceYears = table.Column<int>(type: "INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Candidates", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Vacancies",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Title = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                Requirements = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                SalaryMin = table.Column<decimal>(type: "TEXT", nullable: false),
                SalaryMax = table.Column<decimal>(type: "TEXT", nullable: false),
                Status = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Vacancies", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Applications",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                CandidateId = table.Column<int>(type: "INTEGER", nullable: false),
                VacancyId = table.Column<int>(type: "INTEGER", nullable: false),
                Status = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                AppliedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Applications", x => x.Id);
                table.ForeignKey(
                    name: "FK_Applications_Candidates_CandidateId",
                    column: x => x.CandidateId,
                    principalTable: "Candidates",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Applications_Vacancies_VacancyId",
                    column: x => x.VacancyId,
                    principalTable: "Vacancies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "SoftSkillAssessments",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                CandidateId = table.Column<int>(type: "INTEGER", nullable: false),
                Communication = table.Column<int>(type: "INTEGER", nullable: false),
                Teamwork = table.Column<int>(type: "INTEGER", nullable: false),
                Responsibility = table.Column<int>(type: "INTEGER", nullable: false),
                StressResistance = table.Column<int>(type: "INTEGER", nullable: false),
                Leadership = table.Column<int>(type: "INTEGER", nullable: false),
                OverallComment = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SoftSkillAssessments", x => x.Id);
                table.ForeignKey(
                    name: "FK_SoftSkillAssessments_Candidates_CandidateId",
                    column: x => x.CandidateId,
                    principalTable: "Candidates",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Interviews",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                ApplicationId = table.Column<int>(type: "INTEGER", nullable: false),
                InterviewDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                InterviewType = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                Result = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                Notes = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Interviews", x => x.Id);
                table.ForeignKey(
                    name: "FK_Interviews_Applications_ApplicationId",
                    column: x => x.ApplicationId,
                    principalTable: "Applications",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "InterviewFeedbacks",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                InterviewId = table.Column<int>(type: "INTEGER", nullable: false),
                Comment = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                Score = table.Column<int>(type: "INTEGER", nullable: false),
                Recommendation = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_InterviewFeedbacks", x => x.Id);
                table.ForeignKey(
                    name: "FK_InterviewFeedbacks_Interviews_InterviewId",
                    column: x => x.InterviewId,
                    principalTable: "Interviews",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Applications_CandidateId",
            table: "Applications",
            column: "CandidateId");

        migrationBuilder.CreateIndex(
            name: "IX_Applications_VacancyId",
            table: "Applications",
            column: "VacancyId");

        migrationBuilder.CreateIndex(
            name: "IX_Candidates_Email",
            table: "Candidates",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_InterviewFeedbacks_InterviewId",
            table: "InterviewFeedbacks",
            column: "InterviewId");

        migrationBuilder.CreateIndex(
            name: "IX_Interviews_ApplicationId",
            table: "Interviews",
            column: "ApplicationId");

        migrationBuilder.CreateIndex(
            name: "IX_SoftSkillAssessments_CandidateId",
            table: "SoftSkillAssessments",
            column: "CandidateId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "InterviewFeedbacks");
        migrationBuilder.DropTable(name: "SoftSkillAssessments");
        migrationBuilder.DropTable(name: "Interviews");
        migrationBuilder.DropTable(name: "Applications");
        migrationBuilder.DropTable(name: "Candidates");
        migrationBuilder.DropTable(name: "Vacancies");
    }
}
