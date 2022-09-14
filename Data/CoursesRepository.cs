using AIHR.Test.Models;

namespace AIHR.Test.Data;

public class CoursesRepository
{
    private readonly WorkloadContext _context;

    public CoursesRepository(WorkloadContext context) => _context = context;

    public IEnumerable<Course> GetCourses() => _context.Courses;
}