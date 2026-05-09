using System;
using HRReserveSystem.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRReserveSystem.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20260509193000_AddRecruitersAndResume")]
public partial class AddRecruitersAndResume : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "ResumeSummary",
            table: "Candidates",
            type: "TEXT",
            maxLength: 4000,
            nullable: true);

        migrationBuilder.CreateTable(
            name: "Recruiters",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                FullName = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                Email = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                Login = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                Password = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                Role = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Recruiters", x => x.Id);
            });

        migrationBuilder.AddColumn<int>(
            name: "RecruiterId",
            table: "Interviews",
            type: "INTEGER",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "RecruiterId",
            table: "InterviewFeedbacks",
            type: "INTEGER",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_Recruiters_Email",
            table: "Recruiters",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Recruiters_Login",
            table: "Recruiters",
            column: "Login",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Interviews_RecruiterId",
            table: "Interviews",
            column: "RecruiterId");

        migrationBuilder.CreateIndex(
            name: "IX_InterviewFeedbacks_RecruiterId",
            table: "InterviewFeedbacks",
            column: "RecruiterId");

        // SQLite cannot add foreign keys to existing tables with ALTER TABLE.
        // EF navigation properties and indexes are still used by the application.
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_InterviewFeedbacks_RecruiterId",
            table: "InterviewFeedbacks");

        migrationBuilder.DropIndex(
            name: "IX_Interviews_RecruiterId",
            table: "Interviews");

        migrationBuilder.DropTable(name: "Recruiters");

        migrationBuilder.DropColumn(
            name: "RecruiterId",
            table: "InterviewFeedbacks");

        migrationBuilder.DropColumn(
            name: "RecruiterId",
            table: "Interviews");

        migrationBuilder.DropColumn(
            name: "ResumeSummary",
            table: "Candidates");
    }
}
