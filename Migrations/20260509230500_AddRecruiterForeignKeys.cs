using HRReserveSystem.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRReserveSystem.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20260509230500_AddRecruiterForeignKeys")]
public partial class AddRecruiterForeignKeys : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            """
            PRAGMA foreign_keys=OFF;

            CREATE TABLE "Interviews_new" (
                "Id" INTEGER NOT NULL CONSTRAINT "PK_Interviews" PRIMARY KEY AUTOINCREMENT,
                "ApplicationId" INTEGER NOT NULL,
                "InterviewDate" TEXT NOT NULL,
                "InterviewType" TEXT NOT NULL,
                "Result" TEXT NOT NULL,
                "Notes" TEXT NULL,
                "RecruiterId" INTEGER NULL,
                CONSTRAINT "FK_Interviews_Applications_ApplicationId" FOREIGN KEY ("ApplicationId") REFERENCES "Applications" ("Id") ON DELETE CASCADE,
                CONSTRAINT "FK_Interviews_Recruiters_RecruiterId" FOREIGN KEY ("RecruiterId") REFERENCES "Recruiters" ("Id") ON DELETE SET NULL
            );

            INSERT INTO "Interviews_new" ("Id", "ApplicationId", "InterviewDate", "InterviewType", "Result", "Notes", "RecruiterId")
            SELECT "Id", "ApplicationId", "InterviewDate", "InterviewType", "Result", "Notes", "RecruiterId"
            FROM "Interviews";

            CREATE TABLE "InterviewFeedbacks_new" (
                "Id" INTEGER NOT NULL CONSTRAINT "PK_InterviewFeedbacks" PRIMARY KEY AUTOINCREMENT,
                "InterviewId" INTEGER NOT NULL,
                "Comment" TEXT NOT NULL,
                "Score" INTEGER NOT NULL,
                "Recommendation" TEXT NOT NULL,
                "CreatedAt" TEXT NOT NULL,
                "RecruiterId" INTEGER NULL,
                CONSTRAINT "FK_InterviewFeedbacks_Interviews_InterviewId" FOREIGN KEY ("InterviewId") REFERENCES "Interviews_new" ("Id") ON DELETE CASCADE,
                CONSTRAINT "FK_InterviewFeedbacks_Recruiters_RecruiterId" FOREIGN KEY ("RecruiterId") REFERENCES "Recruiters" ("Id") ON DELETE SET NULL
            );

            INSERT INTO "InterviewFeedbacks_new" ("Id", "InterviewId", "Comment", "Score", "Recommendation", "CreatedAt", "RecruiterId")
            SELECT "Id", "InterviewId", "Comment", "Score", "Recommendation", "CreatedAt", "RecruiterId"
            FROM "InterviewFeedbacks";

            DROP TABLE "InterviewFeedbacks";
            DROP TABLE "Interviews";

            ALTER TABLE "Interviews_new" RENAME TO "Interviews";
            ALTER TABLE "InterviewFeedbacks_new" RENAME TO "InterviewFeedbacks";

            CREATE INDEX "IX_Interviews_ApplicationId" ON "Interviews" ("ApplicationId");
            CREATE INDEX "IX_Interviews_RecruiterId" ON "Interviews" ("RecruiterId");
            CREATE INDEX "IX_InterviewFeedbacks_InterviewId" ON "InterviewFeedbacks" ("InterviewId");
            CREATE INDEX "IX_InterviewFeedbacks_RecruiterId" ON "InterviewFeedbacks" ("RecruiterId");

            PRAGMA foreign_keys=ON;
            """,
            suppressTransaction: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            """
            PRAGMA foreign_keys=OFF;

            CREATE TABLE "Interviews_old" (
                "Id" INTEGER NOT NULL CONSTRAINT "PK_Interviews" PRIMARY KEY AUTOINCREMENT,
                "ApplicationId" INTEGER NOT NULL,
                "InterviewDate" TEXT NOT NULL,
                "InterviewType" TEXT NOT NULL,
                "Result" TEXT NOT NULL,
                "Notes" TEXT NULL,
                "RecruiterId" INTEGER NULL,
                CONSTRAINT "FK_Interviews_Applications_ApplicationId" FOREIGN KEY ("ApplicationId") REFERENCES "Applications" ("Id") ON DELETE CASCADE
            );

            INSERT INTO "Interviews_old" ("Id", "ApplicationId", "InterviewDate", "InterviewType", "Result", "Notes", "RecruiterId")
            SELECT "Id", "ApplicationId", "InterviewDate", "InterviewType", "Result", "Notes", "RecruiterId"
            FROM "Interviews";

            CREATE TABLE "InterviewFeedbacks_old" (
                "Id" INTEGER NOT NULL CONSTRAINT "PK_InterviewFeedbacks" PRIMARY KEY AUTOINCREMENT,
                "InterviewId" INTEGER NOT NULL,
                "Comment" TEXT NOT NULL,
                "Score" INTEGER NOT NULL,
                "Recommendation" TEXT NOT NULL,
                "CreatedAt" TEXT NOT NULL,
                "RecruiterId" INTEGER NULL,
                CONSTRAINT "FK_InterviewFeedbacks_Interviews_InterviewId" FOREIGN KEY ("InterviewId") REFERENCES "Interviews_old" ("Id") ON DELETE CASCADE
            );

            INSERT INTO "InterviewFeedbacks_old" ("Id", "InterviewId", "Comment", "Score", "Recommendation", "CreatedAt", "RecruiterId")
            SELECT "Id", "InterviewId", "Comment", "Score", "Recommendation", "CreatedAt", "RecruiterId"
            FROM "InterviewFeedbacks";

            DROP TABLE "InterviewFeedbacks";
            DROP TABLE "Interviews";

            ALTER TABLE "Interviews_old" RENAME TO "Interviews";
            ALTER TABLE "InterviewFeedbacks_old" RENAME TO "InterviewFeedbacks";

            CREATE INDEX "IX_Interviews_ApplicationId" ON "Interviews" ("ApplicationId");
            CREATE INDEX "IX_Interviews_RecruiterId" ON "Interviews" ("RecruiterId");
            CREATE INDEX "IX_InterviewFeedbacks_InterviewId" ON "InterviewFeedbacks" ("InterviewId");
            CREATE INDEX "IX_InterviewFeedbacks_RecruiterId" ON "InterviewFeedbacks" ("RecruiterId");

            PRAGMA foreign_keys=ON;
            """,
            suppressTransaction: true);
    }
}
