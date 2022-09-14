using AIHR.Test.Models;
using Microsoft.EntityFrameworkCore;

namespace AIHR.Test.Data;

public class StudentsRepository
{
    private readonly WorkloadContext _context;

    public StudentsRepository(WorkloadContext context) => _context = context;

    public IEnumerable<Student> GetStudents() => _context.Students;

    internal async Task Update(int id, DateOnly startDate, DateOnly endDate, int weeklyStudy)
    {
        var student = await _context.Students.SingleAsync(s => s.Id == id);
        student.StartDate = startDate;
        student.EndDate = endDate;
        student.WeeklyStudy = weeklyStudy;
        _context.Update(student);
        await _context.SaveChangesAsync();
    }
}
