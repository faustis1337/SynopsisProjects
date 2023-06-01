using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using NoSql.Entities.Dtos;
using NoSql.Entities.Entities;

namespace NoSql.Repositories;

public class StudentsRepo : IStudentsRepo
{
    private readonly MongoDBContext _mongoDbContext;
    private readonly IMapper _mapper;

    public StudentsRepo(MongoDBContext mongoDbContext,IMapper mapper)
    {
        _mongoDbContext = mongoDbContext;
        _mapper = mapper;
    }
    
    public bool AddStudent(CreateStudentDto createStudentDto)
    {
        var studentEntity = _mapper.Map<StudentEntity>(createStudentDto);
        
        studentEntity.Classes = new List<string>().ToArray();
        try
        {
            _mongoDbContext.StudentsCollection.InsertOne(studentEntity);
            return true;
        }
        catch(Exception e)
        {
            return false;
        }
    }
    
    public UpdateResult UpdateStudent(UpdateStudentDto updateStudentDto)
    {
        var filter = Builders<StudentEntity>.Filter.Where(entity => entity.Id.ToString() == updateStudentDto.Id);
        
        var update = Builders<StudentEntity>.Update
            .Set(s => s.FirstName, updateStudentDto.FirstName)
            .Set(s => s.LastName, updateStudentDto.LastName);

        return _mongoDbContext.StudentsCollection.UpdateOne(filter, update);
    }
    
    public List<StudentEntity> GetAllStudents()
    {
        var s = _mongoDbContext.StudentsCollection.Find(_ => true).ToList().Select(s => new StudentEntity
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Classes = s.Classes
        }).ToList();;
        
        return s;
    }
    

    public DeleteResult DeleteStudent(String id)
    {
        var filter = Builders<StudentEntity>.Filter.Where(entity => entity.Id.ToString() == id);
        var deleteResult = _mongoDbContext.StudentsCollection.DeleteOne(filter);

        if (deleteResult.DeletedCount > 0)
        {
            var classesToUpdate =
                _mongoDbContext.ClassessCollection.Find(x => x.Students.Contains(id)).ToList();

            foreach (var studentClass in classesToUpdate)
            {
                studentClass.Students = studentClass.Students.Where(c => c != id).ToArray();
                _mongoDbContext.ClassessCollection.ReplaceOne(x => x.Id == studentClass.Id, studentClass);
            }
        }

        return deleteResult;
    }
}