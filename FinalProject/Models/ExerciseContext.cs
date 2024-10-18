using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FinalProject.Models
{
    public class ExerciseContext : DbContext
    {
        public ExerciseContext (DbContextOptions<ExerciseContext> options) : base(options) { }

        DbSet<Exercises> Exercises { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
