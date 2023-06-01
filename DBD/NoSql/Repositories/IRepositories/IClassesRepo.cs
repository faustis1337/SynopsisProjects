using MongoDB.Driver;
using NoSql.Entities.Dtos;
using NoSql.Entities.Entities;

namespace NoSql.Repositories;

public interface IClassesRepo
{
    public bool AddClass(CreateClassDto createClassDto);
    public List<ClassEntity> GetAllClasses();
    public DeleteResult DeleteClass(String id);
    public UpdateResult UpdateClass(UpdateClassDto updateClassDto);
    public bool AddStudentToClass(AddStudentToClassDto addStudentToClassDto);
}