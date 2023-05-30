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
    
    [HttpGet("studentsInAClass")] //This gets all the students in a specific class
    
    public List<Students> GetStudentsInClass(int id)
    {
        return _queryRepo.GetStudentsInClass(id);
    }
    [HttpGet("classesOfAStudent")] //this gets all the classes for a student
    
    public List<Classes> GetClassesAStudentDoes(int id)
    {
        return _queryRepo.GetClassesAStudentDoes(id);
    }

    
    [HttpGet("getClassesAStudentTakes")] //this gets a student list with the classes the student takes
    
    public List<StudentsClasses> GetClassesAStudentTakes(int id)
    {
        return _queryRepo.GetClassesAStudentTakes(id);
    }
    
    [HttpGet("getStudentsInAClass")] //this gets a classes list with which students are in it
    
    public List<ClassesStudent> GetStudentsInAClass(int id)
    {
        return _queryRepo.GetStudentsInAClass(id);
    }
        
    [HttpGet("getAStudentItem")] //this gets a student item with the classes they take under them as a list
    
    public StudentsClasses GetAStudentItem(int id)
    {
        return _queryRepo.GetAStudentItem(id);
    }
}