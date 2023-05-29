using Sql.Models;

namespace Sql.Interfaces;

public interface IStudentRepo
{
    List<Students> GetAllStudents();
    void AddStudents(StudentCreateDto studentsCreate);
    void EditStudents(int id, StudentUpdateDto studentUpdateDto);
    
    void Delete(int id);
}