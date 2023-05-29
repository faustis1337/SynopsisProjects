using Microsoft.AspNetCore.Mvc;
using Sql.Interfaces;
using Sql.Models;

namespace Sql.Controllers;

public class StudentsController : ControllerBase
{
    private readonly IStudentRepo _studentRepo;

    public StudentsController(IStudentRepo studentRepo)
    {
        _studentRepo = studentRepo;
    }
    [HttpGet("studentsGet")]
    
    public List<Students> GetAllStudents()
    {
        return _studentRepo.GetAllStudents();
    }

    [HttpPost("studentsCreate")]
    public void CreateStudent(StudentCreateDto studentsCreate)
    {
        _studentRepo.AddStudents(studentsCreate);
    }
    
    [HttpPut("updateStudent")]
    public void UpdateStudent(int id, StudentUpdateDto studentUpdateDto)
    {
        _studentRepo.EditStudents(id, studentUpdateDto);
    }

    [HttpDelete("studentDelete")]
    public void DeleteStudent(int id)
    {
        _studentRepo.Delete(id);
    }
}