using Microsoft.AspNetCore.Mvc;
using Sql.Interfaces;
using Sql.Models;

namespace Sql.Controllers;

public class EnrollmentController : ControllerBase
{
    private readonly IEnrollmentRepo _enrollmentRepo;

    public EnrollmentController(IEnrollmentRepo enrollmentRepo)
    {
        _enrollmentRepo = enrollmentRepo;
    }

    [HttpGet("enrollmentsGet")]
    public  List<Enrollments> GetAllEnrolls()
    {
        return _enrollmentRepo.GetAllEnrolls();
    }
    
    [HttpPost("enrollmentsCreate")]
    public void CreateEnrollment(EnrollmentCreateDto enrollmentCreate)
    {
        _enrollmentRepo.AddStudentToClass(enrollmentCreate);
    }

    [HttpPut("updateEnrollment")]
    public void UpdateStudent(int id, EnrollmentUpdateDto enrollmentUpdateDto)
    {
        _enrollmentRepo.EditEnrollments(id, enrollmentUpdateDto);
    }
    
    [HttpDelete("enrollmentDelete")]
    public void DeleteEnrolment(int id)
    {
        _enrollmentRepo.Delete(id);
    }
}