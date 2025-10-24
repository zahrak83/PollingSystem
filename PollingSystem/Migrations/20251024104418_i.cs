using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PollingSystem.Migrations
{
    /// <inheritdoc />
    public partial class i : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usersurveys_users_UserId",
                table: "usersurveys");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "usersurveys",
                newName: "NormalUserId");

            migrationBuilder.RenameIndex(
                name: "IX_usersurveys_UserId_SurveyId",
                table: "usersurveys",
                newName: "IX_usersurveys_NormalUserId_SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_usersurveys_users_NormalUserId",
                table: "usersurveys",
                column: "NormalUserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usersurveys_users_NormalUserId",
                table: "usersurveys");

            migrationBuilder.RenameColumn(
                name: "NormalUserId",
                table: "usersurveys",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_usersurveys_NormalUserId_SurveyId",
                table: "usersurveys",
                newName: "IX_usersurveys_UserId_SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_usersurveys_users_UserId",
                table: "usersurveys",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
