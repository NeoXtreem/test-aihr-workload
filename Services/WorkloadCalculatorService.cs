using AIHR.Test.Data;
using AIHR.Test.Models;

namespace AIHR.Test.Services;

public class WorkloadCalculatorService
{
    private readonly CoursesRepository _coursesRepository;
    private readonly StudentsRepository _studentsRepository;

    public WorkloadCalculatorService(CoursesRepository coursesRepository, StudentsRepository studentsRepository)
    {
        _coursesRepository = coursesRepository;
        _studentsRepository = studentsRepository;
    }

    internal async Task<int> Calculate(WorkloadInput workloadInput)
    {
        var weeks = (workloadInput.EndDate.DayNumber - workloadInput.StartDate.DayNumber) / 7;
        var weeklyStudy = _coursesRepository.GetCourses().IntersectBy(workloadInput.CourseIds, c => c.Id).Sum(c => c.Hours) / weeks;
        await _studentsRepository.Update(workloadInput.StudentId, workloadInput.StartDate, workloadInput.EndDate, weeklyStudy);

        return weeklyStudy;
    }
}