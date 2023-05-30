using MongoDB.Driver;
using NoSql.Entities;
using NoSql.Entities.Entities;

public class MongoDBContext
{
    private readonly IMongoDatabase _database;

    public MongoDBContext(IMongoDatabase database)
    {
        _database = database;
    }

    public IMongoCollection<StudentEntity> StudentsCollection => _database.GetCollection<StudentEntity>("students");
    public IMongoCollection<ClassEntity> ClassessCollection => _database.GetCollection<ClassEntity>("classes");
}