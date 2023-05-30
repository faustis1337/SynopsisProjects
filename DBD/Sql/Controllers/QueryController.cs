using Microsoft.AspNetCore.Mvc;
using Sql.Interfaces;
using Sql.Models;

namespace Sql.Controllers;

[ApiController]
[Route("api/[controller]")]

public class QueryController : ControllerBase
{
    
    private readonly IQueryRepo _queryRepo;

    public QueryController(IQueryRepo queryRepo)
    {
        _queryRepo = queryRepo;
    }
    
    [HttpGet("studentsInAClass")]
    
    public List<Students> GetStudentsInClass(int id)
    {
        return _queryRepo.GetStudentsInClass(id);
    }
    [HttpGet("classesOfStudent")]
    
    public List<Classes> GetClassesAStudentDoes(int id)
    {
        return _queryRepo.GetClassesAStudentDoes(id);
    }
    [HttpGet("classesOfStudentList")]
    
    public List<StudentsClasses> GetStudentsInClassList(int id)
    {
        return _queryRepo.GetStudentsInClassList(id);
    }
}