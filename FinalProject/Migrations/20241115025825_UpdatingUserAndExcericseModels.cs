using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingUserAndExcericseModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercisesUsers");

            migrationBuilder.CreateTable(
                name: "ExerciseUser",
                columns: table => new
                {
                    ExercisesExerciseID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsersUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseUser", x => new { x.ExercisesExerciseID, x.UsersUserID });
                    table.ForeignKey(
                        name: "FK_ExerciseUser_Exercises_ExercisesExerciseID",
                        column: x => x.ExercisesExerciseID,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseUser_Users_UsersUserID",
                        column: x => x.UsersUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExerciseUser",
                columns: new[] { "ExercisesExerciseID", "UsersUserID" },
                values: new object[,]
                {
                    { "bench", 1 },
                    { "deadlift", 3 },
                    { "squat", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Premium",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseUser_UsersUserID",
                table: "ExerciseUser",
                column: "UsersUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseUser");

            migrationBuilder.CreateTable(
                name: "ExercisesUsers",
                columns: table => new
                {
                    ExercisesExerciseID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsersUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisesUsers", x => new { x.ExercisesExerciseID, x.UsersUserID });
                    table.ForeignKey(
                        name: "FK_ExercisesUsers_Exercises_ExercisesExerciseID",
                        column: x => x.ExercisesExerciseID,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExercisesUsers_Users_UsersUserID",
                        column: x => x.UsersUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExercisesUsers",
                columns: new[] { "ExercisesExerciseID", "UsersUserID" },
                values: new object[,]
                {
                    { "bench", 1 },
                    { "deadlift", 3 },
                    { "squat", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Premium",
                value: false);

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesUsers_UsersUserID",
                table: "ExercisesUsers",
                column: "UsersUserID");
        }
    }
}
