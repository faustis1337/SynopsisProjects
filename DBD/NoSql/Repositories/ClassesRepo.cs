using AutoMapper;
using MongoDB.Driver;
using NoSql.Entities.Dtos;
using NoSql.Entities.Entities;

namespace NoSql.Repositories;

public class ClassesRepo : IClassesRepo
{
    private readonly MongoDBContext _mongoDbContext;
    private readonly IMapper _mapper;

    public ClassesRepo(MongoDBContext mongoDbContext,IMapper mapper)
    {
        _mongoDbContext = mongoDbContext;
        _mapper = mapper;
    }
    
    public bool AddClass(CreateClassDto createClassDto)
    {
        var classEntity = _mapper.Map<ClassEntity>(createClassDto);
        
        classEntity.Students = new List<string>().ToArray();
        
        try
        {
            _mongoDbContext.ClassessCollection.InsertOne(classEntity);
            return true;
        }
        catch(Exception e)
        {
            return false;
        }
    }

    public List<ClassEntity> GetAllClasses()
    {
        var classes = _mongoDbContext.ClassessCollection.Find(_ => true).ToList().Select(c => new ClassEntity
        {
            Id = c.Id,
            ClassInfo = c.ClassInfo,
            ClassName = c.ClassName,
            Students = c.Students
        }).ToList();;
        
        return classes;
    }

    public DeleteResult DeleteClass(string id)
    {
        var filter = Builders<ClassEntity>.Filter.Where(entity => entity.Id.ToString() == id);

        var deleteResult = _mongoDbContext.ClassessCollection.DeleteOne(filter);

        if (deleteResult.DeletedCount > 0)
        {

            var studentsToUpdate =
                _mongoDbContext.StudentsCollection.Find(x => x.Classes.Contains(id)).ToList();

            foreach (var student in studentsToUpdate)
            {
                student.Classes = student.Classes.Where(c => c != id).ToArray();
                _mongoDbContext.StudentsCollection.ReplaceOne(x => x.Id == student.Id, student);
            }
        }

        return deleteResult;
    }

    public UpdateResult UpdateClass(UpdateClassDto updateClassDto)
    {
        var filter = Builders<ClassEntity>.Filter.Where(entity => entity.Id.ToString() == updateClassDto.Id);
        
        var update = Builders<ClassEntity>.Update
            .Set(c => c.ClassInfo, updateClassDto.ClassInfo)
            .Set(c => c.ClassName, updateClassDto.ClassName);

        return _mongoDbContext.ClassessCollection.UpdateOne(filter, update);
    }
    
    public Boolean AddStudentToClass(AddStudentToClassDto addStudentToClassDto)
    {
        var classFilter = Builders<ClassEntity>.Filter.Where(entity => entity.Id.ToString() == addStudentToClassDto.ClassId);
        var classUpdate = Builders<ClassEntity>.Update.Push("Students", addStudentToClassDto.StudentId);
        
        var studentFilter = Builders<StudentEntity>.Filter.Where(entity => entity.Id.ToString() == addStudentToClassDto.StudentId);
        var studentUpdate = Builders<StudentEntity>.Update.Push("Classes", addStudentToClassDto.ClassId);

        
         var classUpdateResult = _mongoDbContext.ClassessCollection.UpdateOne(classFilter, classUpdate);
         var studentUpdateResult = _mongoDbContext.StudentsCollection.UpdateOne(studentFilter, studentUpdate);
        
         if (classUpdateResult.ModifiedCount > 0 && studentUpdateResult.ModifiedCount > 0)
         {
             return true;
        }

        return false;
    }
}