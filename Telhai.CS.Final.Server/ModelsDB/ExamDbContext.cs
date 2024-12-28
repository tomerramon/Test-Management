using Microsoft.EntityFrameworkCore;
using Telhai.CS.Final.Server.Models;

namespace Telhai.CS.Final.Server.ModelsDB
{
    public class ExamDbContext : DbContext
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // delration of the relationship for one exam with many questions.
            modelBuilder.Entity<Exam>()
                .HasMany(e => e.Questions)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // delration of the relationship for one question with many answers.
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // delration of the relationship for one exam with many participations.
            modelBuilder.Entity<Exam>()
                .HasMany(e => e.Participations)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // delration of the relationship for one participation with many errors.
            modelBuilder.Entity<Participation>()
                .HasMany(p => p.Errors)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }


        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Error> Errors { get; set; }



    }
}
