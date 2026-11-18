using GradePilot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GradePilot.Infrastructure.Persistence;

public class GradePilotDbContext : DbContext
{
    public GradePilotDbContext(
        DbContextOptions<GradePilotDbContext> options)
        : base(options)
    {
    }

    public DbSet<Semester> Semesters => Set<Semester>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Semester>(semester =>
        {
            semester.HasKey(s => s.Id);

            semester.OwnsMany(s => s.Courses, course =>
            {
                course.ToTable("GradedCourses");

                course.WithOwner()
                    .HasForeignKey("SemesterId");

                course.Property<Guid>("Id");

                course.HasKey("Id");

                course.Property(c => c.CourseName)
                    .IsRequired();

                    course.Property(c => c.Credits)
    .HasPrecision(5, 2);

course.Property(c => c.Midterm)
    .HasPrecision(5, 2);

course.Property(c => c.Endterm)
    .HasPrecision(5, 2);

course.Property(c => c.FinalExam)
    .HasPrecision(5, 2);
            });
            

            semester.Navigation(s => s.Courses)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}