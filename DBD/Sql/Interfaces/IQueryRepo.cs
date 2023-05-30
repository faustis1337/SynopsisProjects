using Sql.Models;

namespace Sql.Interfaces;

public interface IQueryRepo
{
    List<Students> GetStudentsInClass(int id);
    List<Classes> GetClassesAStudentDoes(int id);
    List<StudentsClasses> GetStudentsInClassList(int id);

}