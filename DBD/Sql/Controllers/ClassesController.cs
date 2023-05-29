using Microsoft.AspNetCore.Mvc;
using Sql.Interfaces;
using Sql.Models;

namespace Sql.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly IClassRepo _classRepo;
    
    public ClassesController(IClassRepo classRepo)
    {
        _classRepo = classRepo;
    }
    
    [HttpGet("classesGet")]
    public  List<Classes> GetAllClasses()
    {
        return _classRepo.GetAllClasses();
    }
    
    [HttpPost("classesCreate")]
    public void CreateClasses(ClassCreateDto classCreate)
    {
        _classRepo.AddClasses(classCreate);
    }
    
    [HttpPut("updateClass")]
    public void UpdateStudent(int id, ClassUpdateDto classUpdateDto)
    {
        _classRepo.EditClasses(id, classUpdateDto);
    }

    [HttpDelete("classDelete")]
    public void DeleteClass(int id)
    {
        _classRepo.Delete(id);
    }
}