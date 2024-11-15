using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    ExerciseID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Reps = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.ExerciseID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyWeight = table.Column<int>(type: "int", nullable: false),
                    P4PStrength = table.Column<int>(type: "int", nullable: false),
                    Premium = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

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
                table: "Exercises",
                columns: new[] { "ExerciseID", "Name", "Reps", "Weight" },
                values: new object[,]
                {
                    { "bench", "Bench", 12, 100 },
                    { "deadlift", "Deadlift", 6, 315 },
                    { "squat", "Squat", 8, 225 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "BodyWeight", "P4PStrength", "Password", "Premium", "Username" },
                values: new object[,]
                {
                    { 1, 220, 50, "Test1", false, "tate.padilla" },
                    { 2, 190, 50, "Test2", false, "tommy.wells" },
                    { 3, 190, 50, "Test3", false, "caden.heidebrink" }
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

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesUsers_UsersUserID",
                table: "ExercisesUsers",
                column: "UsersUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercisesUsers");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
