namespace AIHR.Test.Models;

public class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int WeeklyStudy { get; internal set; }

    public ICollection<Course> Courses { get; set; } = Array.Empty<Course>();
}