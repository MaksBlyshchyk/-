using HRReserveSystem.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRReserveSystem.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20260511120000_AddCandidateResumeFilePath")]
public partial class AddCandidateResumeFilePath : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "ResumeFilePath",
            table: "Candidates",
            type: "TEXT",
            maxLength: 500,
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ResumeFilePath",
            table: "Candidates");
    }
}
