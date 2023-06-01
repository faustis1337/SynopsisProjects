using MongoDB.Bson;
using MongoDB.Driver;
using NoSql.Entities.Dtos;
using NoSql.Entities.Entities;

namespace NoSql.Repositories;

public interface IStudentsRepo
{
    public bool AddStudent(CreateStudentDto createStudentDto);
    public List<StudentEntity> GetAllStudents();
    public DeleteResult DeleteStudent(String id);
    public UpdateResult UpdateStudent(UpdateStudentDto updateStudentDto);
}