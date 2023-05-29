using Sql.Models;

namespace Sql.Interfaces;

public interface IClassRepo
{
    List<Classes> GetAllClasses();
    void AddClasses(ClassCreateDto classCreate);
    void EditClasses(int id, ClassUpdateDto classUpdateDto);
    void Delete(int id);
}