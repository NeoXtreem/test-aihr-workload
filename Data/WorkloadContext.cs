using AIHR.Test.Models;
using Microsoft.EntityFrameworkCore;

namespace AIHR.Test.Data;

public class WorkloadContext : DbContext
{
    public DbSet<Course> Courses { get; set; }

    public DbSet<Student> Students { get; set; }

    public WorkloadContext(DbContextOptions<WorkloadContext> options) : base(options)
    {
    }
}