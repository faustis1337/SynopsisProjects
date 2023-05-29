using Sql.Models;

namespace Sql.Interfaces;

public interface ISqlRepo
{
    List<Students> GetAllStudents();
    List<Classes> GetAllClasses();

    void AddStudents(StudentCreateDto studentsCreate);

    void AddClasses(ClassCreateDto classCreate);

    void AddStudentToClass(EnrollmentCreateDto enrollmentsCreate);
}