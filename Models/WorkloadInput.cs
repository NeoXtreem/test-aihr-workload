namespace AIHR.Test.Models;

public class WorkloadInput
{
    public int StudentId { get; set; }

    public IEnumerable<int> CourseIds { get; set; } = Array.Empty<int>();

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }
}