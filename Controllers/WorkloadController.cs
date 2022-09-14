using AIHR.Test.Data;
using AIHR.Test.Models;
using AIHR.Test.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.Test.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkloadController : ControllerBase
{
    private readonly StudentsRepository _studentsRepository;
    private readonly CoursesRepository _coursesRepository;
    private readonly WorkloadCalculatorService _workloadCalculatorService;

    public WorkloadController(StudentsRepository studentsRepository, CoursesRepository coursesRepository, WorkloadCalculatorService workloadCalculatorService)
    {
        _studentsRepository = studentsRepository;
        _coursesRepository = coursesRepository;
        _workloadCalculatorService = workloadCalculatorService;
    }

    [HttpGet("[action]")]
    public IEnumerable<Student> Students() => _studentsRepository.GetStudents();

    [HttpGet("[action]")]
    public IEnumerable<Course> Courses() => _coursesRepository.GetCourses();

    [HttpPost("[action]")]
    public async Task<int> Calculate([FromBody] WorkloadInput workloadInput) => await _workloadCalculatorService.Calculate(workloadInput);
}