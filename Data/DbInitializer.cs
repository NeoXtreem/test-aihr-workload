using AIHR.Test.Models;

namespace AIHR.Test.Data;

internal static class DbInitializer
{
    public static void Initialize(WorkloadContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if (!context.Students.Any())
        {
            var students = new Student[]
            {
                new() { Name = "Jason Payne" },
                new() { Name = "Marijn Verdult" },
                new() { Name = "Dusanka Durdevic" }
            };

            foreach (var student in students)
            {
                context.Students.Add(student);
            }
        }

        if (!context.Courses.Any())
        {
            var courses = new Course[]
            {
                new() { Name = "Blockchain and HR", Hours = 8 },
                new() { Name = "Compensation & Benefits", Hours = 32 },
                new() { Name = "Digital HR", Hours = 40 },
                new() { Name = "Digital HR Strategy", Hours = 10 },
                new() { Name = "Digital HR Transformation", Hours = 8 },
                new() { Name = "Diversity & Inclusion", Hours = 20 },
                new() { Name = "Employee Experience & Design Thinking", Hours = 12 },
                new() { Name = "Employer Branding", Hours = 6 },
                new() { Name = "Global Data Integrity", Hours = 12 },
                new() { Name = "Hiring & Recruitment Strategy", Hours = 15 },
                new() { Name = "HR Analytics Leader", Hours = 21 },
                new() { Name = "HR Business Partner 2.0", Hours = 40 },
                new() { Name = "HR Data Analyst", Hours = 18 },
                new() { Name = "HR Data Science in R", Hours = 12 },
                new() { Name = "HR Data Visualization", Hours = 12 },
                new() { Name = "HR Metrics & Reporting", Hours = 40 },
                new() { Name = "Learning & Development", Hours = 30 },
                new() { Name = "Organizational Development", Hours = 30 },
                new() { Name = "People Analytics", Hours = 40 },
                new() { Name = "Statistics in HR", Hours = 15 },
                new() { Name = "Strategic HR Leadership", Hours = 34 },
                new() { Name = "Strategic HR Metrics", Hours = 17 },
                new() { Name = "Talent Acquisition", Hours = 40 }
            };

            foreach (var course in courses)
            {
                context.Courses.Add(course);
            }
        }

        context.SaveChanges();
    }
}