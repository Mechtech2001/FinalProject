using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Reflection;

namespace FinalProject.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; } 
        public DbSet<Exercises> Exercises { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Creating many to many relationship between users and exercises group?
            modelBuilder.Entity<Users>()
                .HasMany(u => u.Exercises)
                .WithMany(e => e.Users)
                .UsingEntity<Dictionary<string, object>>(
                "UserExercises", // Name of the linking table
                j => j
                    .HasOne<Exercises>() // Each link has one Exercise
                    .WithMany()          // An Exercise can appear in many links
                    .HasForeignKey("ExerciseID") // FK in the linking table for Exercises
                    .OnDelete(DeleteBehavior.Cascade), // Specify delete behavior
                j => j
                    .HasOne<Users>()    // Each link has one User
                    .WithMany()         // A User can appear in many links
                    .HasForeignKey("UserID") // FK in the linking table for Users
                    .OnDelete(DeleteBehavior.Cascade), // Specify delete behavior
                j =>
                {
                    j.HasKey("UserID", "ExerciseID"); // Composite primary key
                    j.ToTable("UserExercises");       // Name of the linking table
                });

            // Seed data for Users
            modelBuilder.Entity<Users>().HasData(
                new Users { UserID = 1, Username = "tate.padilla", Password = "Test1", BodyWeight = 220, P4PStrength = 50, Premium = false },
                new Users { UserID = 2, Username = "tommy.wells", Password = "Test2",  BodyWeight = 190, P4PStrength = 50, Premium = false },
                new Users { UserID = 3, Username = "caden.heidebrink", Password = "Test3", BodyWeight = 190, P4PStrength = 50, Premium = false }
            );
            

            // Seed data for Exercises
            modelBuilder.Entity<Exercises>().HasData(
                new Exercises { ExerciseID = "bench", Name = "Bench", Weight = 100, Reps = 12 },
                new Exercises { ExerciseID = "squat", Name = "Squat", Weight = 225, Reps = 8 },
                new Exercises { ExerciseID = "deadlift", Name = "Deadlift", Weight = 315, Reps = 6 }
            );

            // Seed the many-to-many relationships (UserExercises) table
            modelBuilder.Entity("UserExercises").HasData(
                new { UserID = 1, ExerciseID = "bench" },
                new { UserID = 2, ExerciseID = "squat" },
                new { UserID = 3, ExerciseID = "deadlift" }
            );

        }


    }
}
