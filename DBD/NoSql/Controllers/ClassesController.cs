using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NoSql.Entities.Dtos;
using NoSql.Entities.Entities;
using NoSql.Repositories;

namespace NoSql.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly IClassesRepo _classesRepo;

    public ClassesController(IClassesRepo classesRepo)
    {
        _classesRepo = classesRepo;
    }

    [HttpPost("classesCreate")]
    public ClassEntity CreateClass(CreateClassDto createClassDto)
    {
        return _classesRepo.AddClass(createClassDto);
    }
    
    [HttpGet("classesGet")]
    public List<ClassEntity> GetAllClasses()
    {
        return _classesRepo.GetAllClasses();
    }
    
    [HttpDelete("classesDelete")]
    public IActionResult DeleteClass(String id)
    {
        var result = _classesRepo.DeleteClass(id);
        if (result.DeletedCount > 0)
        {
            return Ok("Class deleted");
        }
        return NotFound("Class not found or could not be deleted");
    }

    [HttpPut("classesUpdate")]
    public IActionResult CreateStudent(UpdateClassDto updateClassDto)
    {
        var updateResult = _classesRepo.UpdateClass(updateClassDto);
        if (updateResult.ModifiedCount > 0)
        {
            return Ok("Student updated");
        }
        return NotFound("Student not found or could not be updated");
    }
    
    [HttpPost("classesAddStudent")]
    public IActionResult AddStudentToClass(AddStudentToClassDto addStudentToClassDto)
    {
        var result = _classesRepo.AddStudentToClass(addStudentToClassDto);
        if (result)
        {
            return Ok("Student added to class");
        }
        return NotFound("Student not found or could not be added to class");
    }
}