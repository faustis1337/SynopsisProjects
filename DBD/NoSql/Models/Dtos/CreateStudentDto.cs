using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NoSql.Entities.Dtos;

public class CreateStudentDto
{
    public String FirstName { get; set; }
    public String LastName { get; set; }
}