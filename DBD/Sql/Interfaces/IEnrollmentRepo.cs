using Sql.Models;

namespace Sql.Interfaces;

public interface IEnrollmentRepo
{
    List<Enrollments> GetAllEnrolls();

    void AddStudentToClass(EnrollmentCreateDto enrollmentsCreate);
    void EditEnrollments(int id, EnrollmentUpdateDto enrollmentUpdateDto);

    void Delete(int id);

}