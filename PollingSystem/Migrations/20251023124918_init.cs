using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PollingSystem.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "surveys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_surveys_users_AdminId",
                        column: x => x.AdminId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_questions_surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usersurveys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersurveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usersurveys_surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usersurveys_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_options_questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    NormalUserId = table.Column<int>(type: "int", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_votes_options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_votes_surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_votes_users_NormalUserId",
                        column: x => x.NormalUserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "FullName", "Password", "UserType", "Username" },
                values: new object[,]
                {
                    { 1, "John Smith", "1234", "Admin", "admin1" },
                    { 2, "Sarah Johnson", "1234", "Admin", "admin2" },
                    { 3, "Michael Brown", "1234", "NormalUser", "user1" },
                    { 4, "Emily Davis", "1234", "NormalUser", "user2" },
                    { 5, "David Wilson", "1234", "NormalUser", "user3" },
                    { 6, "Sophia Miller", "1234", "NormalUser", "user4" },
                    { 7, "James Anderson", "1234", "NormalUser", "user5" }
                });

            migrationBuilder.InsertData(
                table: "surveys",
                columns: new[] { "Id", "AdminId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Product Quality Evaluation" },
                    { 2, 2, "Professor X Course Feedback" }
                });

            migrationBuilder.InsertData(
                table: "questions",
                columns: new[] { "Id", "SurveyId", "Text" },
                values: new object[,]
                {
                    { 1, 1, "How would you rate the overall quality of our products?" },
                    { 2, 1, "How satisfied are you with the packaging quality?" },
                    { 3, 2, "How would you rate Professor X's knowledge of the subject?" },
                    { 4, 2, "How well does Professor X interact with students?" },
                    { 5, 2, "Overall, how satisfied are you with Professor X's classes?" }
                });

            migrationBuilder.InsertData(
                table: "options",
                columns: new[] { "Id", "QuestionId", "Text" },
                values: new object[,]
                {
                    { 1, 1, "Excellent" },
                    { 2, 1, "Good" },
                    { 3, 1, "Average" },
                    { 4, 1, "Poor" },
                    { 5, 2, "Very well packaged" },
                    { 6, 2, "Good" },
                    { 7, 2, "Could be better" },
                    { 8, 2, "Poor" },
                    { 9, 3, "Excellent" },
                    { 10, 3, "Good" },
                    { 11, 3, "Average" },
                    { 12, 3, "Poor" },
                    { 13, 4, "Very friendly and respectful" },
                    { 14, 4, "Good" },
                    { 15, 4, "Acceptable" },
                    { 16, 4, "Unfriendly" },
                    { 17, 5, "Completely satisfied" },
                    { 18, 5, "Somewhat satisfied" },
                    { 19, 5, "Dissatisfied" },
                    { 20, 5, "Completely dissatisfied" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_options_QuestionId",
                table: "options",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_questions_SurveyId",
                table: "questions",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_surveys_AdminId",
                table: "surveys",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_usersurveys_SurveyId",
                table: "usersurveys",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_usersurveys_UserId_SurveyId",
                table: "usersurveys",
                columns: new[] { "UserId", "SurveyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_votes_NormalUserId",
                table: "votes",
                column: "NormalUserId");

            migrationBuilder.CreateIndex(
                name: "IX_votes_OptionId",
                table: "votes",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_votes_SurveyId",
                table: "votes",
                column: "SurveyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usersurveys");

            migrationBuilder.DropTable(
                name: "votes");

            migrationBuilder.DropTable(
                name: "options");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "surveys");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
