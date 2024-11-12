using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
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
                name: "UserExercises",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ExerciseID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExercises", x => new { x.UserID, x.ExerciseID });
                    table.ForeignKey(
                        name: "FK_UserExercises_Exercises_ExerciseID",
                        column: x => x.ExerciseID,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserExercises_Users_UserID",
                        column: x => x.UserID,
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
                table: "UserExercises",
                columns: new[] { "ExerciseID", "UserID" },
                values: new object[,]
                {
                    { "bench", 1 },
                    { "squat", 2 },
                    { "deadlift", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserExercises_ExerciseID",
                table: "UserExercises",
                column: "ExerciseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserExercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
