using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Reflection;

namespace FinalProject.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        // public DbSet<Users> Users { get; set; } = null!;
        DbSet<Exercises> Exercises { get; set; } = null!;

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Creating many to many relationship between users and exercises group?
            modelBuilder.Entity<Users>()
                .HasMany(u => u.Exercises)
                .WithMany(e => e.Users)
                .UsingEntity(j => j.ToTable("UserExercises"));

            // Seed data for Users
            modelBuilder.Entity<Users>().HasData(
                new Users { UserID = 1, Username = "tate.padilla", Password = "Test1", ExerciseID = "bench", BodyWeight = 220, P4PStrength = 50, Premium = false },
                new Users { UserID = 2, Username = "tommy.wells", Password = "Test2", ExcerciseRoutine = "squat", BodyWeight = 190, P4PStrength = 50, Premium = false },
                new Users { UserID = 3, Username = "caden.heidebrink", Password = "Test3", ExcerciseRoutine = "deadlift", BodyWeight = 190, P4PStrength = 50, Premium = false }
            );
            */

            // Seed data for Exercises
            modelBuilder.Entity<Exercises>().HasData(
                new Exercises { ExerciseID = "bench", Name = "Bench", Weight = 100, Reps = 12 },
                new Exercises { ExerciseID = "squat", Name = "Squat", Weight = 225, Reps = 8 },
                new Exercises { ExerciseID = "deadlift", Name = "Deadlift", Weight = 315, Reps = 6 }
            );
        
        }


    }
}
