using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using NoSql.Entities;
using NoSql.Entities.Dtos;
using NoSql.Entities.Entities;
using NoSql.Repositories;

namespace NoSql.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentsRepo _studentsRepo;

    public StudentsController(IStudentsRepo studentsRepo)
    {
        _studentsRepo = studentsRepo;
    }

    [HttpPost("studentsCreate")]
    public IActionResult AddStudent(CreateStudentDto createStudentDto)
    {

        var isCreated = _studentsRepo.AddStudent(createStudentDto);

        if (!isCreated)
        {
            return StatusCode(500, "Failed to create a student");
        }
        
        return Ok("Student created");
        }
    
    [HttpGet("studentsGet")]
    public List<StudentEntity> GetAllStudents()
    {
        return _studentsRepo.GetAllStudents();
    }
    
    [HttpDelete("studentsDelete")]
    public IActionResult DeleteStudent(String id)
    {
        var result = _studentsRepo.DeleteStudent(id);
        if (result.DeletedCount > 0)
        {
            return Ok("Student deleted");
        }
        return NotFound("Student not found or could not be deleted");
    }

    [HttpPut("studentsUpdate")]
    public IActionResult CreateStudent(UpdateStudentDto updateStudentDto)
    {
        var updateResult = _studentsRepo.UpdateStudent(updateStudentDto);
        if (updateResult.ModifiedCount > 0)
        {
            return Ok("Student updated");
        }
        return NotFound("Student not found or could not be updated");
    }
}

