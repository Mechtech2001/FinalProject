using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Reflection;

namespace FinalProject.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } 
        public DbSet<Exercise> Exercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Users
            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, Username = "tate.padilla", Password = "Test1", BodyWeight = 220, P4PStrength = 50, Premium = true },
                new User { UserID = 2, Username = "tommy.wells", Password = "Test2", BodyWeight = 190, P4PStrength = 50, Premium = false },
                new User { UserID = 3, Username = "caden.heidebrink", Password = "Test3", BodyWeight = 190, P4PStrength = 50, Premium = false }
            );

            // Seed data for Exercises
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise { ExerciseID = "bench", Name = "Bench", Weight = 100, Reps = 12 },
                new Exercise { ExerciseID = "squat", Name = "Squat", Weight = 225, Reps = 8 },
                new Exercise { ExerciseID = "deadlift", Name = "Deadlift", Weight = 315, Reps = 6 }
            );

            // Seed many-to-many relationship data
            modelBuilder.Entity<User>().HasMany(u => u.Exercises).WithMany(e => e.Users)
                .UsingEntity(j => j.HasData(
                    new { UsersUserID = 1, ExercisesExerciseID = "bench" },
                    new { UsersUserID = 2, ExercisesExerciseID = "squat" },
                    new { UsersUserID = 3, ExercisesExerciseID = "deadlift" }
                ));

        }
    }
}
