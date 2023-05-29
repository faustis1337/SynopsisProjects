using Microsoft.AspNetCore.Mvc;
using Sql.Interfaces;
using Sql.Models;

namespace Sql.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SqlController : ControllerBase
{
    private readonly ISqlRepo _sqlRepo;

    public SqlController(ISqlRepo sqlRepo)
    {
        _sqlRepo = sqlRepo;
    }

    [HttpGet("students")]
    public List<Students> GetAllStudents()
    {
        return _sqlRepo.GetAllStudents();
    }

    [HttpGet("classes")]
    public  List<Classes> GetAllClasses()
    {
        return _sqlRepo.GetAllClasses();
    }

    [HttpPost("students")]
    public void CreateStudent(StudentCreateDto studentsCreate)
    {
        _sqlRepo.AddStudents(studentsCreate);
    }

    [HttpPost("classes")]
    public void CreateClasses(ClassCreateDto classCreate)
    {
        _sqlRepo.AddClasses(classCreate);
    }

    [HttpPost("enrollments")]
    public void CreateEnrollment(EnrollmentCreateDto enrollmentCreate)
    {
        _sqlRepo.AddStudentToClass(enrollmentCreate);
    }
    
}