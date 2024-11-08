using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Reflection;

namespace FinalProject.Models
{
    public class ExerciseContext : DbContext
    {
        public ExerciseContext(DbContextOptions<ExerciseContext> options) : base(options) { }

        DbSet<Exercises> Exercises { get; set; } = null!;

        public DbSet<Users> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Users
            modelBuilder.Entity<Exercises>().HasData(
                new Exercises { ExerciseID = 1, Name = "Bench", Weight = 100, Reps = 12 },
                new Exercises { ExerciseID = 1, Name = "Squat", Weight = 225, Reps = 8 },
                new Exercises { ExerciseID = 1, Name = "Deadlift", Weight = 315, Reps = 6 }
            );


            // Seed data for Users
            modelBuilder.Entity<Users>().HasData(
                new Users { UserID = 1, Username = "tate.padilla", Password = "Test1", ExcerciseRoutine = "???", BodyWeight = 220, P4PStrength = 50, Premium = false },
                new Users { UserID = 2, Username = "tommy.wells", Password = "Test2", ExcerciseRoutine = "???", BodyWeight = 190, P4PStrength = 50, Premium = false },
                new Users { UserID = 3, Username = "caden.heidebrink", Password = "Test3", ExcerciseRoutine = "???", BodyWeight = 190, P4PStrength = 50, Premium = false }
            );
        }


    }
}
